using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Pokatun.Core.Enums;
using Pokatun.Core.Factories;
using Pokatun.Data;
using RestSharp;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Services
{
    public sealed class RestService : IRestService
    {
        private readonly IRestClient _restClient;
        private readonly ISecureStorage _secureStorage;

        public RestService(IRestClientFactory restClientFactory, ISecureStorage secureStorage)
        {
            _restClient = restClientFactory.GetRestClient(Api.Pokatun);
            _secureStorage = secureStorage;
        }

        public  Task<ServerResponce<T>> GetAsync<T>(string path, bool needAuth = true)
        {
            return MakeRequestAsync<T>(Method.GET, path, null, needAuth);
        }

        public Task<ServerResponce<T>> DeleteAsync<T>(string path, long id, bool needAuth = true)
        {
            return MakeRequestAsync<T>(Method.DELETE, path + '/' + id, null, needAuth);
        }

        public Task<ServerResponce<T>> PostAsync<T>(string path, object body, bool needAuth = true)
        {
            return MakeRequestAsync<T>(Method.POST, path, body, needAuth);
        }

        public Task<ServerResponce<T>> PutAsync<T>(string path, long id, object body, bool needAuth = true)
        {
            return MakeRequestAsync<T>(Method.PUT, path + '/' + id, body, needAuth);
        }

        private async Task<ServerResponce<T>> MakeRequestAsync<T>(Method method, string path, object body = null, bool needAuth = true)
        {
            RestRequest request = new RestRequest(path, method);

            if (needAuth)
            {
                request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));
            }

            request.AddHeader("Content-Type", "application/json");

            if (body != null)
            {
                request.AddJsonBody(body, "application/json");
            }

            IRestResponse<ServerResponce<T>> response = await _restClient.ExecuteAsync<ServerResponce<T>>(request);

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
                return new ServerResponce<T> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }
    }
}
