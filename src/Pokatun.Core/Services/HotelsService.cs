using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;
using RestSharp;

namespace Pokatun.Core.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly IRestClient _restClient;

        public HotelsService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ServerResponce<TokenInfoDto>> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(password));
            }

            RestRequest request = new RestRequest("hotels/login", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new HotelDto { Email = email, Password = password }, "application/json");
            
            IRestResponse<ServerResponce<TokenInfoDto>> response = await _restClient.ExecuteAsync<ServerResponce<TokenInfoDto>>(request);

            if (response.Data == null && response.Content.Contains("Exception"))
            {
                return new ServerResponce<TokenInfoDto> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }

        public async Task<ServerResponce<TokenInfoDto>> RegisterAsync(HotelDto hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            RestRequest request = new RestRequest("hotels/register", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(hotel, "application/json");

            IRestResponse<ServerResponce<TokenInfoDto>> response = await _restClient.ExecuteAsync<ServerResponce<TokenInfoDto>>(request);

            if (response.Data == null && response.Content.Contains("Exception"))
            {
                return new ServerResponce<TokenInfoDto> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }

        public async Task<ServerResponce> ForgotPasswordAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            RestRequest request = new RestRequest("hotels/forgot-password", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new ForgotPasswordRequest { Email = email }, "application/json");

            IRestResponse<ServerResponce<object>> response = await _restClient.ExecuteAsync<ServerResponce<object>>(request);

            if (response.Data == null && response.Content.Contains("Exception"))
            {
                return new ServerResponce { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }

        public async Task<ServerResponce> ValidateResetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            RestRequest request = new RestRequest("hotels/validate-reset-token", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new ValidateResetTokenRequest { Token = token }, "application/json");

            IRestResponse<ServerResponce<object>> response = await _restClient.ExecuteAsync<ServerResponce<object>>(request);

            if (response.Data == null && response.Content.Contains("Exception"))
            {
                return new ServerResponce { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }
    }
}
