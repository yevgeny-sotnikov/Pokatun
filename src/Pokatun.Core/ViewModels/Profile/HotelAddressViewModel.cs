using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class HotelAddressViewModel : BaseViewModelResult<AddressDto>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IGeocodingService _geocodingService;

        public override string Title => Strings.HotelLocationAddress;

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (!SetProperty(ref _searchText, value) || value.Length < 3)
                    return;

                OnSearchAdresses(value);
            }
        }

        public MvxObservableCollection<AddressDto> FoundAdresses { get; private set; }

        private MvxAsyncCommand<AddressDto> _addressSelectedCommand;
        public IMvxAsyncCommand<AddressDto> AddressSelectedCommand
        {
            get
            {
                return _addressSelectedCommand ?? (_addressSelectedCommand = new MvxAsyncCommand<AddressDto>(DoAddressSelectedCommandAsync));
            }
        }

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        public HotelAddressViewModel(IMvxNavigationService navigationService, IGeocodingService geocodingService)
        {
            _navigationService = navigationService;
            _geocodingService = geocodingService;

            FoundAdresses = new MvxObservableCollection<AddressDto>();
        }

        private async void OnSearchAdresses(string text)
        {
            FoundAdresses.ReplaceWith(await _geocodingService.GetAdressesAsync(text));
        }

        private Task DoAddressSelectedCommandAsync(AddressDto param)
        {
            return _navigationService.Close(this, param);
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
