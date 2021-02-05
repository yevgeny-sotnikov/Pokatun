using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly IRestService _restService;
        private readonly IPhotosService _photosService;

        public HotelsService(IRestService restService, IPhotosService photosService)
        {
            _restService = restService;
            _photosService = photosService;
        }

        public Task<ServerResponce<HotelDto>> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            return _restService.GetAsync<HotelDto>("hotels/" + id);
        }

        public Task<ServerResponce<HotelShortInfoDto>> GetShortInfoAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            return _restService.GetAsync<HotelShortInfoDto>("hotels/shortinfo/" + id);
        }

        public Task<ServerResponce<TokenInfoDto>> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(password));
            }

            return _restService.PostAsync<TokenInfoDto>("hotels/login", new LoginDto { Email = email, Password = password }, false);
        }

        public Task<ServerResponce<TokenInfoDto>> RegisterAsync(
            string hotelName,
            string fullCompanyName,
            string email,
            string password,
            string phoneNumber,
            string bankName,
            string IBAN,
            long? bankCard,
            int USREOU)
        {
            return _restService.PostAsync<TokenInfoDto>("hotels/register",
                new HotelRegistrationDto
                {
                    HotelName = hotelName,
                    FullCompanyName = fullCompanyName,
                    Email = email,
                    Password = password,
                    Phones = new List<PhoneDto> { new PhoneDto { Number = phoneNumber } },
                    BankName = bankName,
                    IBAN = IBAN,
                    BankCard = bankCard,
                    USREOU = USREOU
                },
                false
            );
        }

        public async Task<ServerResponce> ForgotPasswordAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            return await _restService.PostAsync<object>("hotels/forgot-password", new ForgotPasswordRequest { Email = email }, false);
        }

        public async Task<ServerResponce> ValidateResetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            return await _restService.PostAsync<object>("hotels/validate-reset-token", new ValidateResetTokenRequest { Token = token }, false);
        }

        public Task<ServerResponce<TokenInfoDto>> ResetPassword(string token, string password)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(password));
            }

            return _restService.PostAsync<TokenInfoDto>("hotels/reset-password", new ResetPasswordRequest { Token = token, Password = password }, false);
        }

        public async Task<ServerResponce<string>> SaveChangesAsync(
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
            string photoFileName)
        {
            string nameForSave;

            if (File.Exists(photoFileName))
            {
                ServerResponce<string> fileResponce = await _photosService.UploadAsync(photoFileName);

                if (!fileResponce.Success)
                {
                    return new ServerResponce<string> { ErrorCodes = fileResponce.ErrorCodes };
                }

                nameForSave = fileResponce.Data;
            } 
            else nameForSave = photoFileName;

            ServerResponce<string> responce = await _restService.PostAsync<string>("hotels", new HotelDto
            {
                Id = currentHotelId,
                HotelName = hotelName,
                FullCompanyName = fullCompanyName,
                Email = email,
                BankName = bankName,
                IBAN = IBAN,
                BankCard = bankCard,
                USREOU = USREOU,
                Phones = new List<PhoneDto>(phones),
                SocialResources = new List<SocialResourceDto>(socialResources),
                CheckInTime = checkInTime,
                CheckOutTime = checkOutTime,
                WithinTerritoryDescription = withinTerritoryDescription,
                HotelDescription = hotelDescription,
                Address = hotelLocation.Addres,
                Longtitude = hotelLocation.Longtitude,
                Latitude = hotelLocation.Latitude,
                PhotoUrl = nameForSave
            });

            responce.Data = nameForSave;

            return responce;
        }
    }
}
