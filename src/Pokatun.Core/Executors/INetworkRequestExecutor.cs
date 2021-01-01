using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface INetworkRequestExecutor
    {
        Task<T> MakeRequestAsync<T>(Func<Task<T>> func, ISet<string> knownErrorCodes) where T : ServerResponce;
    }
}
