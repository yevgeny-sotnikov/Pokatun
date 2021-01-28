using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class HotelAddressViewModel : BaseViewModelResult<AddressDto>
    {
        private readonly IMvxNavigationService _navigationService;

        public override string Title => Strings.HotelLocationAddress;

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        public HotelAddressViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }

    }
}
