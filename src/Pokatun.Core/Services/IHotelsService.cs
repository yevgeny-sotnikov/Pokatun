using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IHotelsService
    {
        Task<ServerResponce<HotelDto>> GetAsync(long id);

        Task<ServerResponce<ShortInfoDto>> GetShortInfoAsync(long id);

        Task<ServerResponce<TokenInfoDto>> RegisterAsync(
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

        Task<ServerResponce<string>> SaveChangesAsync(
            long currentHotelId,
            string hotelName,
            string fullCompanyName,
            string email,
            string bankName,
            string IBAN,
            long? bankCard,
            int USREOU,
            IEnumerable<PhoneDto> phones,
            IEnumerable<SocialResourceDto> socialResources,
            TimeSpan checkInTime,
            TimeSpan checkOutTime,
            string withinTerritoryDescription,
            string hotelDescription,
            LocationDto hotelLocation,
            string photoFilePath
        );
    }
}
