using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface IAuthExecutor
    {
        Task<TokenInfoDto> MakeAuthAsync(Func<Task<ServerResponce<TokenInfoDto>>> func, ISet<string> knownErrorCodes);
    }
}
