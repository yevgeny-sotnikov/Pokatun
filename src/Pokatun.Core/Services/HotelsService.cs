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

            return PostAsync<TokenInfoDto>("hotels/login", new LoginDto { Email = email, Password = password });
        }

        public Task<ServerResponce<TokenInfoDto>> RegisterAsync(HotelDto hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            return PostAsync<TokenInfoDto>("hotels/register", hotel);
        }

        public async Task<ServerResponce> ForgotPasswordAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            return await PostAsync<object>("hotels/forgot-password", new ForgotPasswordRequest { Email = email });
        }

        public async Task<ServerResponce> ValidateResetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            return await PostAsync<object>("hotels/validate-reset-token", new ValidateResetTokenRequest { Token = token });
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

            return PostAsync<TokenInfoDto>("hotels/reset-password", new ResetPasswordRequest { Token = token, Password = password });
        }

        private async Task<ServerResponce<T>> PostAsync<T>(string path, object body)
        {
            RestRequest request = new RestRequest(path, Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body, "application/json");

            IRestResponse<ServerResponce<T>> response = await _restClient.ExecuteAsync<ServerResponce<T>>(request);

            if (string.IsNullOrWhiteSpace(response.Content) || response.Content.Contains("Exception"))
            {
                return new ServerResponce<T> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }
    }
}
