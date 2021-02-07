using System.Collections.Generic;
using System.Linq;
using Pokatun.API.Entities;
using Pokatun.API.Models;
using Pokatun.Data;
using Microsoft.EntityFrameworkCore;

namespace Pokatun.API.Services
{
    public sealed class HotelsApiService : IHotelsApiService
    {
        private readonly PokatunContext _context;

        public HotelsApiService(PokatunContext context)
        {
            _context = context;
        }

        public HotelDto GetById(long hotelId)
        {
            if (hotelId <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Hotel hotel = _context.Hotels
                .Include(h => h.Phones)
                .Include(h => h.SocialResources)
                .Include(h => h.Account)
                .FirstOrDefault(h => h.Id == hotelId);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            return new HotelDto
            {
                Id = hotel.Id,
                HotelName = hotel.HotelName,
                Phones = new List<PhoneDto>(hotel.Phones.Select(p => new PhoneDto { Id = p.Id, Number = p.Number })),
                SocialResources = new List<SocialResourceDto>(hotel.SocialResources.Select(sr => new SocialResourceDto { Id = sr.Id, Link = sr.Link })),
                Email = hotel.Account.Email,
                FullCompanyName = hotel.FullCompanyName,
                BankCard = hotel.BankCard,
                IBAN = hotel.IBAN,
                BankName = hotel.BankName,
                USREOU = hotel.USREOU,
                CheckInTime = hotel.CheckInTime,
                CheckOutTime = hotel.CheckOutTime,
                HotelDescription = hotel.HotelDescription,
                WithinTerritoryDescription = hotel.WithinTerritoryDescription,
                Address = hotel.Address,
                Longtitude = hotel.Longtitude,
                Latitude = hotel.Latitude,
                PhotoUrl = hotel.Account.PhotoName
            };
        }

        public HotelShortInfoDto GetShortInfo(long id)
        {
            if (id <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Hotel hotel = _context.Hotels.Include(h => h.Account)
                .FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            bool anyPhones = _context.Phones.Any(phone => phone.HotelId == hotel.Id);
            bool anySocialResources = _context.SocialResources.Any(sr => sr.HotelId == hotel.Id);

            return new HotelShortInfoDto
            {
                HotelName = hotel.HotelName,
                PhotoName = hotel.Account.PhotoName,
                Address = hotel.Address,
                HotelNumbersAmount = _context.HotelNumbers.Count(x => x.HotelId == id),
                CheckInTime = hotel.CheckInTime,
                CheckOutTime = hotel.CheckOutTime,
                ProfileNotCompleted = (hotel.BankCard == null && hotel.IBAN == null)
                    || hotel.BankName == null
                    || hotel.CheckInTime == null
                    || hotel.CheckOutTime == null
                    || hotel.FullCompanyName == null
                    || hotel.HotelDescription == null
                    || hotel.HotelName == null
                    || hotel.Account.PhotoName == null
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
                .Include(h => h.Account)
                .FirstOrDefault(h => h.Id == hotelDto.Id);

            if (hotel == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            hotel.BankCard = hotelDto.BankCard;
            hotel.BankName = hotelDto.BankName;
            hotel.CheckInTime = hotelDto.CheckInTime;
            hotel.CheckOutTime = hotelDto.CheckOutTime;
            hotel.Account.Email = hotelDto.Email;
            hotel.FullCompanyName = hotelDto.FullCompanyName;
            hotel.HotelDescription = hotelDto.HotelDescription;
            hotel.HotelName = hotelDto.HotelName;
            hotel.IBAN = hotelDto.IBAN;
            hotel.USREOU = hotelDto.USREOU;
            hotel.Account.PhotoName = hotelDto.PhotoUrl;
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
    }
}
