using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public class TouristsService : ITouristsService
    {
        private readonly IRestService _restService;

        public TouristsService(IRestService restService)
        {
            _restService = restService;
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

    }
}
