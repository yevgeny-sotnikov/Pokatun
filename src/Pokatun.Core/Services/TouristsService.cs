using System;
using System.IO;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public class TouristsService : ITouristsService
    {
        private readonly IRestService _restService;
        private readonly IPhotosService _photosService;

        public TouristsService(IRestService restService, IPhotosService photosService)
        {
            _restService = restService;
            _photosService = photosService;
        }

        public Task<ServerResponce<TouristDto>> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            return _restService.GetAsync<TouristDto>("tourists/" + id);
        }

        public Task<ServerResponce<TouristShortInfoDto>> GetShortInfoAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            return _restService.GetAsync<TouristShortInfoDto>("tourists/shortinfo/" + id);
        }

        public async Task<ServerResponce<string>> SaveChangesAsync(long currentTouristId, string fullName, string phone, string email, string photoFileName)
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

            ServerResponce<string> responce = await _restService.PostAsync<string>("tourists", new TouristDto
            {
                Id = currentTouristId,
                FullName = fullName,
                PhoneNumber = phone,
                Email = email,
                PhotoName = nameForSave
            });

            responce.Data = nameForSave ?? string.Empty;

            return responce;
        }
    }
}
