using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Pokatun.API.Entities;
using Pokatun.API.Models;
using Pokatun.Data;
using Microsoft.EntityFrameworkCore;

namespace Pokatun.API.Services
{
    public sealed class HotelsApiService : IHotelsApiService
    {
        private readonly PokatunContext _context;
        private readonly IEmailApiService _emailService;

        public HotelsApiService(PokatunContext context, IEmailApiService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public Hotel GetById(long hotelId)
        {
            if (hotelId <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Hotel hotel = _context.Hotels
                .Include(h => h.Phones)
                .Include(h => h.SocialResources)
                .FirstOrDefault(h => h.Id == hotelId);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            return hotel;
        }

        public ShortInfoDto GetShortInfo(long id)
        {
            if (id <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Hotel hotel = _context.Hotels
                .FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            bool anyPhones = _context.Phones.Any(phone => phone.HotelId == hotel.Id);
            bool anySocialResources = _context.SocialResources.Any(sr => sr.HotelId == hotel.Id);

            return new ShortInfoDto
            {
                HotelName = hotel.HotelName,
                PhotoName = hotel.PhotoUrl,
                Address = hotel.Address,
                ProfileNotCompleted = (hotel.BankCard == null && hotel.IBAN == null)
                    || hotel.BankName == null
                    || hotel.CheckInTime == null
                    || hotel.CheckOutTime == null
                    || hotel.Email == null
                    || hotel.FullCompanyName == null
                    || hotel.HotelDescription == null
                    || hotel.HotelName == null
                    || hotel.PhotoUrl == null
                    || hotel.USREOU == 0
                    || hotel.WithinTerritoryDescription == null
                    || !anyPhones
                    || string.IsNullOrWhiteSpace(hotel.Address)
                    || hotel.Longtitude == null
                    || hotel.Latitude == null
                    || !anySocialResources
            };
        }

        public void Update(HotelDto hotelDto)
        {
            Hotel hotel = _context.Hotels
                .Include(h => h.Phones)
                .Include(h => h.SocialResources)
                .FirstOrDefault(h => h.Id == hotelDto.Id);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            hotel.BankCard = hotelDto.BankCard;
            hotel.BankName = hotelDto.BankName;
            hotel.CheckInTime = hotelDto.CheckInTime;
            hotel.CheckOutTime = hotelDto.CheckOutTime;
            hotel.Email = hotelDto.Email;
            hotel.FullCompanyName = hotelDto.FullCompanyName;
            hotel.HotelDescription = hotelDto.HotelDescription;
            hotel.HotelName = hotelDto.HotelName;
            hotel.IBAN = hotelDto.IBAN;
            hotel.USREOU = hotelDto.USREOU;
            hotel.PhotoUrl = hotelDto.PhotoUrl;
            hotel.WithinTerritoryDescription = hotelDto.WithinTerritoryDescription;
            hotel.Address = hotelDto.Address;
            hotel.Longtitude = hotelDto.Longtitude;
            hotel.Latitude = hotelDto.Latitude;

            IDictionary<long, Phone> dbPhones = hotel.Phones.ToDictionary(phone => phone.Id);
            IDictionary<long, PhoneDto> dtoPhones = hotelDto.Phones.Where(p => p.Id != 0).ToDictionary(phone => phone.Id);
            
            foreach (PhoneDto phoneDto in hotelDto.Phones)
            {
                if (dbPhones.ContainsKey(phoneDto.Id))
                {
                    dbPhones[phoneDto.Id].Number = phoneDto.Number;
                }
                else
                {
                    hotel.Phones.Add(new Phone { Number = phoneDto.Number });
                }
            }

            foreach (Phone phone in dbPhones.Values)
            {
                if (!dtoPhones.ContainsKey(phone.Id))
                {
                    hotel.Phones.Remove(phone);
                }
            }

            IDictionary<long, SocialResource> dbSocialResources = hotel.SocialResources.ToDictionary(sr => sr.Id);
            IDictionary<long, SocialResourceDto> dtoSocialResources = hotelDto.SocialResources.Where(sr => sr.Id != 0).ToDictionary(sr => sr.Id);

            foreach (SocialResourceDto srDto in hotelDto.SocialResources)
            {
                if (dbSocialResources.ContainsKey(srDto.Id))
                {
                    dbSocialResources[srDto.Id].Link = srDto.Link;
                }
                else
                {
                    hotel.SocialResources.Add(new SocialResource { Link = srDto.Link });
                }
            }

            foreach (SocialResource sr in dbSocialResources.Values)
            {
                if (!dtoSocialResources.ContainsKey(sr.Id))
                {
                    hotel.SocialResources.Remove(sr);
                }
            }

            _context.SaveChanges();
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

        public long Register(HotelRegistrationDto value)
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
                Phones = new List<Phone>(value.Phones.Select(p => new Phone { Id = p.Id, Number = p.Number })),
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

        public Hotel ValidateResetToken(string token)
        {
            Hotel hotel = _context.Hotels.SingleOrDefault(x => x.ResetToken == token);

            if (hotel == null)
                throw new ApiException(ErrorCodes.InvalidTokenError);

            if (hotel.ResetTokenExpires < DateTime.UtcNow)
                throw new ApiException(ErrorCodes.ExpiredTokenError);

            return hotel;
        }

        public long ResetPassword(string token, string password)
        {
            Hotel hotel = ValidateResetToken(token);

            using (HMACSHA512 hmac = new HMACSHA512())
            {
                hotel.PasswordSalt = hmac.Key;
                hotel.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            hotel.ResetToken = null;
            hotel.ResetTokenExpires = null;

            _context.Hotels.Update(hotel);
            _context.SaveChanges();

            return hotel.Id;
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
    }
}
