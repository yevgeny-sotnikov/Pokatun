using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Pokatun.API.Entities;
using Pokatun.API.Models;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public sealed class HotelsService : IHotelsService
    {
        private readonly PokatunContext _context;
        private readonly IEmailService _emailService;

        public HotelsService(PokatunContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public Hotel GetById(long userId)
        {
            return _context.Hotels.Find(userId);
        }

        public long Login(string email, string password)
        {
            email = email.ToLower();

            Hotel hotel = _context.Hotels.SingleOrDefault(x => x.Email == email);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            using (var hmac = new HMACSHA512(hotel.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hotel.PasswordHash[i])
                    {
                        throw new ApiException(ErrorCodes.IncorrectPasswordError);
                    }
                }
            }

            return hotel.Id;
        }

        public long Register(HotelDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.Email = value.Email.ToLower();

            IList<string> errors = new List<string>(3);

            if (_context.Hotels.Any(hotel => hotel.Email == value.Email))
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

            Hotel hotel = new Hotel
            {
                HotelName = value.HotelName,
                PhoneNumber = value.PhoneNumber,
                Email = value.Email,
                FullCompanyName = value.FullCompanyName,
                BankCard = value.BankCard,
                IBAN = value.IBAN,
                BankName = value.BankName,
                USREOU = value.USREOU
            };

            using (HMACSHA512 hmac = new HMACSHA512())
            {
                hotel.PasswordSalt = hmac.Key;
                hotel.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value.Password));
            }

            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            return hotel.Id;
        }

        public void ForgotPassword(string email)
        {
            email = email.ToLower();

            Hotel hotel = _context.Hotels.SingleOrDefault(x => x.Email == email);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            string resetToken;

            do
            {
                resetToken = GenerateRandomTokenString();
            }
            while (_context.Hotels.Any(h => h.ResetToken == resetToken));

            hotel.ResetToken = resetToken;
            hotel.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);

            _context.Hotels.Update(hotel);
            _context.SaveChanges();

            _emailService.Send(hotel.Email, "Sign-up Verification API - Reset Password", "Verification code: " + resetToken);
        }

        private string GenerateRandomTokenString()
        {
            var randomBytes = new byte[4];

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(randomBytes);
            }

            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
