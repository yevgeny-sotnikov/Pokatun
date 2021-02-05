using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface ITouristService
    {
        Task<ServerResponce<TouristShortInfoDto>> GetShortInfoAsync(long id);
    }
}
