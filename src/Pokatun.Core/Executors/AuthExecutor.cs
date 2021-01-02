using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Executors
{
    public sealed class AuthExecutor : IAuthExecutor
    {
        private readonly IHotelsService _hotelsService;
        private readonly ISecureStorage _secureStorage;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;

        public AuthExecutor(
            IHotelsService hotelsService,
            ISecureStorage secureStorage,
            INetworkRequestExecutor networkRequestExecutor,
            IUserDialogs userDialogs,
            IMvxNavigationService navigationService)
        {
            _hotelsService = hotelsService;
            _secureStorage = secureStorage;
            _networkRequestExecutor = networkRequestExecutor;
            _userDialogs = userDialogs;
            _navigationService = navigationService;
        }

        public async Task MakeAuthAsync(Func<Task<ServerResponce<TokenInfoDto>>> func, ISet<string> knownErrorCodes, IMvxViewModel closeViewModel, bool needLoadShortInfo = true)
        {
            if (closeViewModel == null)
            {
                throw new ArgumentNullException(nameof(closeViewModel));
            }

            ShortInfoDto shortInfo = null;

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                ServerResponce<TokenInfoDto> responce = await _networkRequestExecutor.MakeRequestAsync(func, knownErrorCodes, false);

                if (responce == null)
                    return;

                TokenInfoDto dto = responce.Data;

                await _secureStorage.SetAsync(Constants.Keys.AccountId, dto.AccountId.ToString(CultureInfo.InvariantCulture));
                await _secureStorage.SetAsync(Constants.Keys.Token, dto.Token);
                await _secureStorage.SetAsync(
                    Constants.Keys.TokenExpirationTime,
                    dto.ExpirationTime.ToUniversalTime().ToString(CultureInfo.InvariantCulture)
                );

                if (needLoadShortInfo)
                {
                    ServerResponce<ShortInfoDto> serverResponce;

                    do
                    {
                        serverResponce = await _hotelsService.GetShortInfoAsync(dto.AccountId);
                    }
                    while (!serverResponce.Success);

                    shortInfo = serverResponce.Data;
                }
            }

            await _navigationService.Close(closeViewModel);
            await _navigationService.Navigate<HotelMenuViewModel, ShortInfoDto>(shortInfo);
        }
    }
}
