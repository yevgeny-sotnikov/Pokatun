using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface ITouristsService
    {
        Task<ServerResponce<TouristShortInfoDto>> GetShortInfoAsync(long id);

        Task<ServerResponce<TouristDto>> GetAsync(long touristId);
    }
}
