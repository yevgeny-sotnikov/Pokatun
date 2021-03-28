using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IBidsService
    {
        Task<ServerResponce> AddNewAsync(long hotelNumberId, long price, byte discount, IEnumerable<TimeRangeDto> timeRanges);

        Task<ServerResponce> UpdateExistsAsync(long bidId, long price, byte discount, DateTime minDate, DateTime maxDate);

        Task<ServerResponce<List<BidDto>>> GetAllAsync();
    }
}
