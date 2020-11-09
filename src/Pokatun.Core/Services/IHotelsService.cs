using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IHotelsService
    {
        Task<ServerResponce<TokenInfoDto>> RegisterAsync(HotelDto hotel);
        Task<ServerResponce<TokenInfoDto>> LoginAsync(string email, string password);
    }
}
