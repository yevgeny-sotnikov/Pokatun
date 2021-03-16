using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly IRestService _restService;
        private readonly IPhotosService _photosService;

        public HotelsService(IRestService restService, IPhotosService photosService)
        {
            _restService = restService;
            _photosService = photosService;
        }

        public Task<ServerResponce<HotelDto>> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            return _restService.GetAsync<HotelDto>("hotels/" + id);
        }

        public Task<ServerResponce<HotelShortInfoDto>> GetShortInfoAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(id));
            }

            return _restService.GetAsync<HotelShortInfoDto>("hotels/shortinfo/" + id);
        }

        public async Task<ServerResponce<string>> SaveChangesAsync(
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
            LocationDto hotelLocation,
            string photoFileName)
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

            ServerResponce<string> responce = await _restService.PostAsync<string>("hotels", new HotelDto
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
                Address = hotelLocation.Addres,
                Longtitude = hotelLocation.Longtitude,
                Latitude = hotelLocation.Latitude,
                PhotoUrl = nameForSave
            });

            responce.Data = nameForSave ?? string.Empty;

            return responce;
        }
    }
}
