using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public class BidsService : IBidsService
    {
        private readonly IRestService _restService;

        public BidsService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<ServerResponce> AddNewAsync(long hotelNumberId, long price, byte discount, IEnumerable<TimeRangeDto> timeRanges)
        {
            return await _restService.PostAsync<object>(
                "bids",
                new CreateBidsDto
                {
                    HotelNumberId = hotelNumberId,
                    Price = price,
                    Discount = discount,
                    TimeRanges = new List<TimeRangeDto>(timeRanges)
                }
            );

        }

        public Task<ServerResponce<List<BidDto>>> GetAllAsync()
        {
            return _restService.GetAsync<List<BidDto>>("bids");
        }
    }
}
