using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IGeocodingService
    {
        Task<LocationDto[]> GetLocationsAsync(string stringForSearch);
    }
}
