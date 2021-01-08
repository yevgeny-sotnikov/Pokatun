using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public sealed class NetworkRequestExecutor : INetworkRequestExecutor
    {
        private IUserDialogs _userDialogs;

        public NetworkRequestExecutor(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        public async Task<T> MakeRequestAsync<T>(Func<Task<T>> func, ISet<string> knownErrorCodes, bool withLoading = true) where T : ServerResponce
        {
            Task<T> task = withLoading ? CallFuncWithLoadingAsync(func) : func();

            T responce = await task;

            if (responce.Success)
            {
                return responce;
            }

            knownErrorCodes.IntersectWith(responce.ErrorCodes);

            if (knownErrorCodes.Count > 0)
            {
                _userDialogs.Toast(Strings.ResourceManager.GetString(knownErrorCodes.First()));
                return null;
            }

            _userDialogs.Toast(Strings.UnexpectedError);

            return null;
        }

        private async Task<T> CallFuncWithLoadingAsync<T>(Func<Task<T>> func) where T : ServerResponce
        {
            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                return await func();
            }
        }
    }
}
