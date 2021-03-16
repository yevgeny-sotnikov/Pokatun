using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IAccountsService
    {
        Task<ServerResponce<TokenInfoDto>> RegisterTouristAsync(string fullname, string email, string password, string phoneNumber);

        Task<ServerResponce<TokenInfoDto>> RegisterHotelAsync(
            string hotelName,
            string fullCompanyName,
            string email,
            string password,
            string phoneNumber,
            string bankName,
            string IBAN,
            long? bankCard,
            int USREOU
        );

        Task<ServerResponce<TokenInfoDto>> LoginAsync(string email, string password);

        Task<ServerResponce> ForgotPasswordAsync(string email);

        Task<ServerResponce> ValidateResetToken(string code);

        Task<ServerResponce<TokenInfoDto>> ResetPassword(string token, string password);
    }
}
