using System.Collections.Generic;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelNumbersApiService
    {
        void AddNew(long hotelId, HotelNumberDto value);

        List<HotelNumberDto> GetAll(long hotelId);

        void Delete(long id);
    }
}