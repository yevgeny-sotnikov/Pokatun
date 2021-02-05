using Pokatun.API.Entities;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelsApiService
    {
        HotelDto GetById(long id);

        HotelShortInfoDto GetShortInfo(long id);

        void Update(HotelDto hotel);

    }
}
