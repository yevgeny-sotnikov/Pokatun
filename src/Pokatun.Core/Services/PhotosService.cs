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
    public sealed class PhotosService : IPhotosService
    {
        private readonly IRestClient _restClient;
        private readonly ISecureStorage _secureStorage;

        public PhotosService(IRestClientFactory restClientFactory, ISecureStorage secureStorage)
        {
            _restClient = restClientFactory.GetRestClient(Api.Pokatun);
            _secureStorage = secureStorage;
        }

        public async Task<Stream> GetAsync(string photoName)
        {
            if (string.IsNullOrWhiteSpace(photoName))
            {
                throw new ArgumentException(nameof(photoName) + " doesn't exist");
            }

            RestRequest request = new RestRequest("photos?name=" + photoName, Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));

            IRestResponse restResponse = await _restClient.ExecuteAsync(request);

            if (!restResponse.IsSuccessful)
            {
                return null;
            }

            return new MemoryStream(restResponse.RawBytes);
        }

        public async Task<ServerResponce<string>> UploadAsync(string photofilePath)
        {
            if (!File.Exists(photofilePath))
            {
                throw new InvalidOperationException("We can upload only local image files");
            }    

            RestRequest request = new RestRequest("photos", Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", await _secureStorage.GetAsync(Constants.Keys.Token)));
            request.AddHeader("Content-Type", "multipart/form-data");

            request.AddFile("file", photofilePath);

            IRestResponse<ServerResponce<string>> response = await _restClient.ExecuteAsync<ServerResponce<string>>(request);

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
                return new ServerResponce<string> { ErrorCodes = new List<string> { ErrorCodes.UnknownError } };
            }

            return response.Data;
        }
    }
}
