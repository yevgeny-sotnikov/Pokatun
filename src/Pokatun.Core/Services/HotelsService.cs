using System;
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

        public async Task<string> RegisterAsync(Hotel hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            RestRequest request = new RestRequest("hotels/register", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(hotel, "application/json");
            IRestResponse<string> response = await _restClient.ExecuteAsync<string>(request);
            return response.Data;
        }
    }
}
