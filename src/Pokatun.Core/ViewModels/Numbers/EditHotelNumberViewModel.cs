using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class EditHotelNumberViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        public EditHotelNumberViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
