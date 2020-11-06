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

        public async Task<ServerResponce<TokenInfoDto>> RegisterAsync(HotelDto hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            //System.Security.Cryptography.HMACSHA512()

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
    }
}
