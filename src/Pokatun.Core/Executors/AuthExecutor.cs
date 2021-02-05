using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Executors
{
    public sealed class AuthExecutor : IAuthExecutor
    {
        private readonly ISecureStorage _secureStorage;
        private readonly INetworkRequestExecutor _networkRequestExecutor;

        public AuthExecutor(ISecureStorage secureStorage, INetworkRequestExecutor networkRequestExecutor)
        {
            _secureStorage = secureStorage;
            _networkRequestExecutor = networkRequestExecutor;
        }

        public async Task<TokenInfoDto> MakeAuthAsync(Func<Task<ServerResponce<TokenInfoDto>>> func, ISet<string> knownErrorCodes)
        {
            ServerResponce<TokenInfoDto> responce = await _networkRequestExecutor.MakeRequestAsync(func, knownErrorCodes, false);

            if (responce == null)
                return null;

            TokenInfoDto dto = responce.Data;

            await _secureStorage.SetAsync(Constants.Keys.AccountId, dto.AccountId.ToString(CultureInfo.InvariantCulture));
            await _secureStorage.SetAsync(Constants.Keys.Token, dto.Token);
            await _secureStorage.SetAsync(
                Constants.Keys.TokenExpirationTime,
                dto.ExpirationTime.ToUniversalTime().ToString(CultureInfo.InvariantCulture)
            );

            return dto;
        }
    }
}
