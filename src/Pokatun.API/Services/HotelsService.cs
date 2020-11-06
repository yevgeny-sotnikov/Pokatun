using System;
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

        public HotelsService(PokatunContext context)
        {
            _context = context;
        }

        public Hotel GetById(long userId)
        {
            return _context.Hotels.Find(userId);
        }

        public long RegisterAsync(HotelDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.Email = value.Email.ToLower();

            if (_context.Hotels.Any(hotel => hotel.Email == value.Email))
            {
                throw new ApiException(ErrorCodes.AccountAllreadyExistsError);
            }

            if (value.IBAN != null && _context.Hotels.Any(hotel => hotel.IBAN == value.IBAN))
            {
                throw new ApiException(ErrorCodes.IbanAllreadyRegisteredError);
            }

            if (_context.Hotels.Any(hotel => hotel.USREOU == value.USREOU))
            {
                throw new ApiException(ErrorCodes.UsreouAllreadyRegisteredError);
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
    }
}
