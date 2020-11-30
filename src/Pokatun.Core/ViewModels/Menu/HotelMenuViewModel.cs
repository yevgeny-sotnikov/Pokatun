using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Menu
{
    public sealed class HotelMenuViewModel : BaseViewModel
    {
        private readonly  IMvxNavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;

        private MvxAsyncCommand _exitCommand;
        public IMvxAsyncCommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new MvxAsyncCommand(DoExitCommandAsync));
            }
        }

        public HotelMenuViewModel(IMvxNavigationService navigationService, ISecureStorage secureStorage)
        {
            _navigationService = navigationService;
            _secureStorage = secureStorage;
        }

        private Task DoExitCommandAsync()
        {
            _secureStorage.RemoveAll();

            return _navigationService.Navigate<ChoiseUserRoleViewModel>();
        }
    }
}
