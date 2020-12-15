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

        public async Task<T> MakeRequestAsync<T>(Func<Task<T>> func, ISet<string> knownErrorCodes) where T : ServerResponce
        {
            T responce = null;

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                responce = await func();

                if (responce.Success)
                {
                    return responce;
                }
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
    }
}
