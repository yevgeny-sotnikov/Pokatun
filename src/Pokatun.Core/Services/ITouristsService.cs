using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface ITouristsService
    {
        Task<ServerResponce<TouristShortInfoDto>> GetShortInfoAsync(long id);

        Task<ServerResponce<TouristDto>> GetAsync(long touristId);

        Task<ServerResponce<string>> SaveChangesAsync(long currentTouristId, string fullName, string phone, string email, string photoFileName);
    }
}
