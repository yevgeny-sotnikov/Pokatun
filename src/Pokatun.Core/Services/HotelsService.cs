using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Pokatun.Data;
using RestSharp;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly IRestClient _restClient;
        private readonly ISecureStorage _secureStorage;
        private readonly IPhotosService _photosService;
        private readonly System.IO.Abstractions.IFileSystem _fileSystem;

        public HotelsService(IRestClient restClient, ISecureStorage secureStorage, IPhotosService photosService, System.IO.Abstractions.IFileSystem fileSystem)
        {
            _restClient = restClient;
            _secureStorage = secureStorage;
            _photosService = photosService;
            _fileSystem = fileSystem;
        }

        public async Task<ServerResponce<HotelDto>> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            RestRequest request = new RestRequest("hotels/" + id, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));

            IRestResponse<ServerResponce<HotelDto>> response = await _restClient.ExecuteAsync<ServerResponce<HotelDto>>(request);

            if (response.ErrorException != null)
            {
                Crashes.TrackError(response.ErrorException);
            }
            else if (response.Content.Contains("Exception"))
            {
                Crashes.TrackError(new Exception(response.Content));
            }

            if (string.IsNullOrWhiteSpace(response.Content) || response.ErrorException != null || response.Content.Contains("Exception"))
            {
                return new ServerResponce<HotelDto> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }

        public async Task<ServerResponce<ShortInfoDto>> GetShortInfoAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            RestRequest request = new RestRequest("hotels/shortinfo/" + id, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));

            IRestResponse<ServerResponce<ShortInfoDto>> response = await _restClient.ExecuteAsync<ServerResponce<ShortInfoDto>>(request);

            if (response.ErrorException != null)
            {
                Crashes.TrackError(response.ErrorException);
                Console.WriteLine(response.ErrorException);
            }
            else if (response.Content.Contains("Exception"))
            {
                Crashes.TrackError(new Exception(response.Content));
                Console.WriteLine(response.Content);
            }

            if (string.IsNullOrWhiteSpace(response.Content) || response.ErrorException != null || response.Content.Contains("Exception"))
            {
                return new ServerResponce<ShortInfoDto> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
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

            return PostAsync<TokenInfoDto>("hotels/login", new LoginDto { Email = email, Password = password }, false);
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

            return PostAsync<TokenInfoDto>("hotels/register",
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

            return await PostAsync<object>("hotels/forgot-password", new ForgotPasswordRequest { Email = email }, false);
        }

        public async Task<ServerResponce> ValidateResetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            return await PostAsync<object>("hotels/validate-reset-token", new ValidateResetTokenRequest { Token = token }, false);
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

            return PostAsync<TokenInfoDto>("hotels/reset-password", new ResetPasswordRequest { Token = token, Password = password }, false);
        }

        public async Task<ServerResponce> SaveChangesAsync(
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
            string photoFileName)
        {
            string nameForSave;

            if (_fileSystem.File.Exists(photoFileName))
            {
                ServerResponce<string> fileResponce = await _photosService.UploadAsync(photoFileName);

                if (!fileResponce.Success)
                {
                    return new ServerResponce { ErrorCodes = fileResponce.ErrorCodes };
                }

                nameForSave = fileResponce.Data;
            }
            else nameForSave = photoFileName;

            return await PostAsync<object>("hotels", new HotelDto
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
                PhotoUrl = nameForSave
            });
        }

        private async Task<ServerResponce<T>> PostAsync<T>(string path, object body, bool needAuth = true)
        {
            RestRequest request = new RestRequest(path, Method.POST);

            if (needAuth)
            {
                request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));
            }

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body, "application/json");

            IRestResponse<ServerResponce<T>> response = await _restClient.ExecuteAsync<ServerResponce<T>>(request);

            if (response.ErrorException != null)
            {
                Crashes.TrackError(response.ErrorException);
            }
            else if (response.Content.Contains("Exception"))
            {
                Crashes.TrackError(new Exception(response.Content));
            }

            if (string.IsNullOrWhiteSpace(response.Content) || response.ErrorException != null || response.Content.Contains("Exception"))
            {
                return new ServerResponce<T> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }
    }
}
