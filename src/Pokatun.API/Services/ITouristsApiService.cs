using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface ITouristsApiService
    {
        TouristDto GetById(long id);

        TouristShortInfoDto GetShortInfo(long id);

        void Update(TouristDto tourist);
    }
}
