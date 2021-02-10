using System;
using System.Collections.Generic;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IBidsApiService
    {
        void AddNew(CreateBidsDto value);

        List<BidDto> GetAll(long v);
    }
}
