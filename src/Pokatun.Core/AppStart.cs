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
        private readonly IPreferences _preferences;
        private readonly IHotelsService _hotelsService;
        private readonly ITouristsService _touristService;
        private readonly IMemoryCache _memoryCache;

        public AppStart(
            IMvxApplication app,
            IMvxNavigationService navigationService,
            ISecureStorage secureStorage,
            IPreferences preferences,
            IHotelsService hotelsService,
            ITouristsService touristService,
            IMemoryCache memoryCache
        ) : base(app, navigationService)
        {
            _secureStorage = secureStorage;
            _preferences = preferences;
            _hotelsService = hotelsService;
            _touristService = touristService;
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

            UserRole role = (UserRole)_preferences.Get(Constants.Keys.UserRole, 0);

            if (!Enum.IsDefined(typeof(UserRole), role))
            {
                await NavigationService.Navigate<ChoiseUserRoleViewModel>();
                return;
            }

            if (role == UserRole.HotelAdministrator)
            {
                await NavigateToFirstPageAsync<HotelMenuViewModel, HotelShortInfoDto>(
                    Constants.Keys.ShortHotelInfo,
                    async () => await _hotelsService.GetShortInfoAsync(long.Parse(await _secureStorage.GetAsync(Constants.Keys.AccountId)))
                );
            }
            else
            {
                await NavigateToFirstPageAsync<TouristMenuViewModel, TouristShortInfoDto>(
                    Constants.Keys.ShortTouristInfo,
                    async () => await _touristService.GetShortInfoAsync(long.Parse(await _secureStorage.GetAsync(Constants.Keys.AccountId)))
                );
            }
        }

        private async Task NavigateToFirstPageAsync<TViewModel, TShortInfo>(string key, Func<Task<ServerResponce<TShortInfo>>> func) where TViewModel : IMvxViewModel<TShortInfo>
        {
            TShortInfo dto = _memoryCache.Get<TShortInfo>(key);

            if (dto != null)
            {
                await NavigationService.Navigate<TViewModel, TShortInfo>(dto);
                return;
            }

            TaskCompletionSource<TShortInfo> tcs = new TaskCompletionSource<TShortInfo>();

            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            Task.Run(async () =>
            {
                #if DEBUG //This code needed for debug mobile and backed on local machine. We offer to back 1000ms for initialization

                await Task.Delay(1000);

                #endif

                //TODO: add fail case handling            
                ServerResponce<TShortInfo> serverResponce = await func();

                _memoryCache.Set(key, serverResponce.Data);
                tcs.SetResult(serverResponce.Data);
            });

            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            await NavigationService.Navigate<TViewModel, TShortInfo>(tcs.Task.Result);
        }
    }
}