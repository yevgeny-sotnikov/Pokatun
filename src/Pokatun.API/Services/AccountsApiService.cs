using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pokatun.API.Entities;
using Pokatun.API.Helpers;
using Pokatun.API.Models;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public sealed class AccountsApiService : IAccountsApiService
    {
        private readonly PokatunContext _context;
        private readonly AppSettings _appSettings;
        private readonly IEmailApiService _emailService;

        public AccountsApiService(PokatunContext context, IOptions<AppSettings> appSettings, IEmailApiService emailService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        public TokenInfoDto RegisterNewTourist(TouristRegistrationDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.Email = value.Email.ToLower();

            IList<string> errors = new List<string>();

            if (_context.Accounts.Any(acc => acc.Email == value.Email))
            {
                errors.Add(ErrorCodes.AccountAllreadyExistsError);
            }

            if (errors.Any())
            {
                throw new ApiException(errors);
            }

            long touristId;

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    long accountId = CreateAccount(value.Email, value.Password, UserRole.Tourist);

                    Tourist tourist = new Tourist
                    {
                        FullName = value.FullName,
                        PhoneNumber = value.PhoneNumber,
                        AccountId = accountId
                    };

                    _context.Tourists.Add(tourist);
                    _context.SaveChanges();

                    transaction.Commit();

                    touristId = tourist.Id;
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
            }

            return HandleAuthorizationRequest(touristId, UserRole.Tourist);
        }

        public TokenInfoDto RegisterNewHotel(HotelRegistrationDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.Email = value.Email.ToLower();

            IList<string> errors = new List<string>(3);

            if (_context.Accounts.Any(acc => acc.Email == value.Email))
            {
                errors.Add(ErrorCodes.AccountAllreadyExistsError);
            }

            if (value.IBAN != null && _context.Hotels.Any(hotel => hotel.IBAN == value.IBAN))
            {
                errors.Add(ErrorCodes.IbanAllreadyRegisteredError);
            }

            if (_context.Hotels.Any(hotel => hotel.USREOU == value.USREOU))
            {
                errors.Add(ErrorCodes.UsreouAllreadyRegisteredError);
            }

            if (errors.Any())
            {
                throw new ApiException(errors);
            }

            long hotelId;

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    long accountId = CreateAccount(value.Email, value.Password, UserRole.HotelAdministrator);

                    Hotel hotel = new Hotel
                    {
                        HotelName = value.HotelName,
                        Phones = new List<Phone>(value.Phones.Select(p => new Phone { Id = p.Id, Number = p.Number })),
                        FullCompanyName = value.FullCompanyName,
                        BankCard = value.BankCard,
                        IBAN = value.IBAN,
                        BankName = value.BankName,
                        USREOU = value.USREOU,
                        AccountId = accountId
                    };

                    _context.Hotels.Add(hotel);
                    _context.SaveChanges();

                    transaction.Commit();

                    hotelId = hotel.Id;
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
            }

            return HandleAuthorizationRequest(hotelId, UserRole.HotelAdministrator);
        }

        public TokenInfoDto Login(string email, string password)
        {
            email = email.ToLower();

            Account account = _context.Accounts.SingleOrDefault(x => x.Email == email);

            if (account == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            using (var hmac = new HMACSHA512(account.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != account.PasswordHash[i])
                    {
                        throw new ApiException(ErrorCodes.IncorrectPasswordError);
                    }
                }
            }

            return ReadRelatedToAccountDataIdAndGenerateToken(account);
        }

        public void ForgotPassword(string email)
        {
            email = email.ToLower();

            Account account = _context.Accounts.SingleOrDefault(x => x.Email == email);

            if (account == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            string resetToken;

            do
            {
                resetToken = GenerateRandomTokenString();
            }
            while (_context.Accounts.Any(h => h.ResetToken == resetToken));

            account.ResetToken = resetToken;
            account.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);

            _context.Accounts.Update(account);
            _context.SaveChanges();

            _emailService.Send(account.Email, "Sign-up Verification API - Reset Password", "Verification code: " + resetToken);
        }

        public Account ValidateResetToken(string token)
        {
            Account account = _context.Accounts.SingleOrDefault(x => x.ResetToken == token);

            if (account == null)
                throw new ApiException(ErrorCodes.InvalidTokenError);

            if (account.ResetTokenExpires < DateTime.UtcNow)
                throw new ApiException(ErrorCodes.ExpiredTokenError);

            return account;
        }

        public TokenInfoDto ResetPassword(string token, string password)
        {
            Account account = ValidateResetToken(token);

            using (HMACSHA512 hmac = new HMACSHA512())
            {
                account.PasswordSalt = hmac.Key;
                account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            account.ResetToken = null;
            account.ResetTokenExpires = null;

            _context.Accounts.Update(account);
            _context.SaveChanges();

            return ReadRelatedToAccountDataIdAndGenerateToken(account);
        }

        private TokenInfoDto ReadRelatedToAccountDataIdAndGenerateToken(Account account)
        {
            long id;

            if (account.Role == UserRole.Tourist)
            {
                id = _context.Tourists.First(x => x.AccountId == account.Id).Id;
            }
            else
            {
                id = _context.Hotels.First(x => x.AccountId == account.Id).Id;
            }

            return HandleAuthorizationRequest(id, account.Role);
        }

        private string GenerateRandomTokenString()
        {
            var randomBytes = new byte[4];

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(randomBytes);
            }

            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        }

        private TokenInfoDto HandleAuthorizationRequest(long id, UserRole userRole)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            DateTime expireTime = DateTime.UtcNow.AddDays(_appSettings.TokenExpirationDays);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, id.ToString()) }),
                Expires = expireTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            string tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new TokenInfoDto
            {
                AccountId = id,
                Role = userRole,
                Token = tokenString,
                ExpirationTime = expireTime
            };
        }

        private long CreateAccount(string email, string password, UserRole role)
        {
            Account account = new Account
            {
                Role = role,
                Email = email
            };

            using (HMACSHA512 hmac = new HMACSHA512())
            {
                account.PasswordSalt = hmac.Key;
                account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account.Id;
        }
    }
}
