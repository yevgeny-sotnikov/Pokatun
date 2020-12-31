using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Pokatun.Data;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Services
{
    public sealed class PhotosService : IPhotosService
    {
        private readonly IRestClient _restClient;
        private readonly ISecureStorage _secureStorage;

        public PhotosService(IRestClient restClient, ISecureStorage secureStorage)
        {
            _restClient = restClient;
            _secureStorage = secureStorage;
        }

        public async Task<ServerResponce<string>> UploadAsync(string photofilePath)
        {
            if (Uri.IsWellFormedUriString(photofilePath, UriKind.Absolute))
            {
                return new ServerResponce<string> { Data = photofilePath };
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
