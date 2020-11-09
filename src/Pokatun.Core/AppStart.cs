using System;
using System.Globalization;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Menu;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly ISecureStorage _secureStorage;

        public AppStart(IMvxApplication app, IMvxNavigationService navigationService, ISecureStorage secureStorage) : base(app, navigationService)
        {
            _secureStorage = secureStorage;
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            if (string.IsNullOrWhiteSpace(await _secureStorage.GetAsync(Constants.Keys.Token)))
            {
                await NavigationService.Navigate<ChoiseUserRoleViewModel>();
                return;
            }

            DateTime utcTime = DateTime.Parse(await _secureStorage.GetAsync(Constants.Keys.TokenExpirationTime), CultureInfo.InvariantCulture);
            if (utcTime < DateTime.UtcNow)
            {
                await NavigationService.Navigate<ChoiseUserRoleViewModel>();
            }
            else
            {
                await NavigationService.Navigate<HotelMenuViewModel>();
            }
        }
    }
}