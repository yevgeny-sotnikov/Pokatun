using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class EditHotelProfileViewModel : BaseViewModelResult<bool>
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

        public EditHotelProfileViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this, false);
        }
    }
}
