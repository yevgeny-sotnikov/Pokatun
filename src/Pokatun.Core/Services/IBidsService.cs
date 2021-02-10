using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IBidsService
    {
        Task<ServerResponce> AddNewAsync(long hotelNumberId, long price, byte discount, IEnumerable<TimeRangeDto> timeRanges);
    }
}
