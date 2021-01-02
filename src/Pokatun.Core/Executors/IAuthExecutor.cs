using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public interface IAuthExecutor
    {
        Task MakeAuthAsync(Func<Task<ServerResponce<TokenInfoDto>>> func, ISet<string> knownErrorCodes, IMvxViewModel closeViewModel, bool needLoadShortInfo = true);
    }
}
