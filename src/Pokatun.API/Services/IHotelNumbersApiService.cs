using System.Collections.Generic;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelNumbersApiService
    {
        void AddNew(long hotelId, HotelNumberDto value);

        List<HotelNumberDto> GetAll(long hotelId, bool withBids);

        void Delete(long id);

        void UpdateExists(long hotelId, long hotelNumberId, HotelNumberDto value);
    }
}