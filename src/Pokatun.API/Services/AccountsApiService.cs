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

        public AccountsApiService(PokatunContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
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
                    Account account = new Account();
                    account.Email = value.Email;

                    using (HMACSHA512 hmac = new HMACSHA512())
                    {
                        account.PasswordSalt = hmac.Key;
                        account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value.Password));
                    }

                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    Tourist tourist = new Tourist
                    {
                        FullName = value.FullName,
                        PhoneNumber = value.PhoneNumber,
                        AccountId = account.Id
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

            return HandleAuthorizationRequest(touristId);
        }

        private TokenInfoDto HandleAuthorizationRequest(long id)
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
                Token = tokenString,
                ExpirationTime = expireTime
            };
        }
    }
}
