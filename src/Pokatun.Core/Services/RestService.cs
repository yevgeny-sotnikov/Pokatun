using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Pokatun.Data;
using RestSharp;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Services
{
    public sealed class RestService : IRestService
    {
        private readonly IRestClient _restClient;
        private readonly ISecureStorage _secureStorage;

        public RestService(IRestClient restClient, ISecureStorage secureStorage)
        {
            _restClient = restClient;
            _secureStorage = secureStorage;
        }

        public  Task<ServerResponce<T>> GetAsync<T>(string path, bool needAuth = true)
        {
            return MakeNoBodyrequestAsync<T>(Method.GET, path, needAuth);
        }

        public Task<ServerResponce<T>> DeleteAsync<T>(string path, long id, bool needAuth = true)
        {
            return MakeNoBodyrequestAsync<T>(Method.DELETE, path + '/' + id, needAuth);
        }

        public async Task<ServerResponce<T>> PostAsync<T>(string path, object body, bool needAuth = true)
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

        public async Task<ServerResponce<T>> MakeNoBodyrequestAsync<T>(Method method, string path, bool needAuth = true)
        {
            RestRequest request = new RestRequest(path, method);

            if (needAuth)
            {
                request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));
            }

            request.AddHeader("Content-Type", "application/json");

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
