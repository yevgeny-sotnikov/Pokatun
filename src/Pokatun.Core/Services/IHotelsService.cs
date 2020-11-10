using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IHotelsService
    {
        Task<ServerResponce<TokenInfoDto>> RegisterAsync(HotelDto hotel);

        Task<ServerResponce<TokenInfoDto>> LoginAsync(string email, string password);

        Task<ServerResponce> ForgotPasswordAsync(string email);

        Task<ServerResponce> ValidateResetToken(string code);

        Task<ServerResponce<TokenInfoDto>> ResetPassword(string token, string password);
    }
}
