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
            await (string.IsNullOrWhiteSpace(await _secureStorage.GetAsync(Constants.Keys.Token))
                ? NavigationService.Navigate<ChoiseUserRoleViewModel>()
                : NavigationService.Navigate<HotelMenuViewModel>());
        }
    }
}