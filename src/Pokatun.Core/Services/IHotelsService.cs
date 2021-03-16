using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IHotelsService
    {
        Task<ServerResponce<HotelDto>> GetAsync(long id);

        Task<ServerResponce<HotelShortInfoDto>> GetShortInfoAsync(long id);

        Task<ServerResponce<string>> SaveChangesAsync(
            long currentHotelId,
            string hotelName,
            string fullCompanyName,
            string email,
            string bankName,
            string IBAN,
            long? bankCard,
            int USREOU,
            IEnumerable<PhoneDto> phones,
            IEnumerable<SocialResourceDto> socialResources,
            TimeSpan checkInTime,
            TimeSpan checkOutTime,
            string withinTerritoryDescription,
            string hotelDescription,
            LocationDto hotelLocation,
            string photoFilePath
        );
    }
}
