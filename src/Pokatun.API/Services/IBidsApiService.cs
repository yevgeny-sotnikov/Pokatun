using System;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IBidsApiService
    {
        void AddNew(CreateBidsDto value);
    }
}
