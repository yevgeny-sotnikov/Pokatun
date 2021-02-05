using System;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface ITouristsApiService
    {
        TouristShortInfoDto GetShortInfo(long id);
    }
}
