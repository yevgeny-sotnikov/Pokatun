using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly ISecureStorage _secureStorage;
        private readonly IHotelsService _hotelsService;
        private readonly IMemoryCache _memoryCache;

        public AppStart(IMvxApplication app, IMvxNavigationService navigationService, ISecureStorage secureStorage, IHotelsService hotelsService, IMemoryCache memoryCache) : base(app, navigationService)
        {
            _secureStorage = secureStorage;
            _hotelsService = hotelsService;
            _memoryCache = memoryCache;
        }

        protected override Task<object> ApplicationStartup(object hint = null)
        {
            return base.ApplicationStartup(hint);
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            if (string.IsNullOrWhiteSpace(await _secureStorage.GetAsync(Constants.Keys.Token)))
            {
                await NavigationService.Navigate<ChoiseUserRoleViewModel>();
                return;
            }

            string expirationTimeString = await _secureStorage.GetAsync(Constants.Keys.TokenExpirationTime);

            if (string.IsNullOrWhiteSpace(expirationTimeString))
            {
                await NavigationService.Navigate<ChoiseUserRoleViewModel>();
                return;
            }

            DateTime utcTime = DateTime.Parse(expirationTimeString, CultureInfo.InvariantCulture);
            if (utcTime < DateTime.UtcNow)
            {
                await NavigationService.Navigate<ChoiseUserRoleViewModel>();
                return;
            }

            ShortInfoDto dto = _memoryCache.Get<ShortInfoDto>(Constants.Keys.ShortHotelInfo);

            if (dto != null)
            {
                await NavigationService.Navigate<HotelMenuViewModel, ShortInfoDto>(dto);
                return;
            }

            TaskCompletionSource<ShortInfoDto> tcs = new TaskCompletionSource<ShortInfoDto>();

            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            Task.Run(async () =>
            {
                //TODO: add fail case handling            

                #if DEBUG //This code needed for debug mobile and backed on local machine. We offer to back 300ms for initialization

                await Task.Delay(300);

                #endif

                ServerResponce<ShortInfoDto> serverResponce = await _hotelsService.GetShortInfoAsync(
                    long.Parse(await _secureStorage.GetAsync(Constants.Keys.AccountId))
                );

                _memoryCache.Set(Constants.Keys.ShortHotelInfo, serverResponce.Data);
                tcs.SetResult(serverResponce.Data);
            });

            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            await NavigationService.Navigate<HotelMenuViewModel, ShortInfoDto>(tcs.Task.Result);
        }
    }
}