using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelNumbersApiService
    {
        void AddNew(long hotelId, HotelNumberDto value);
    }
}