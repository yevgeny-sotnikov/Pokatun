using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class HotelAddressViewModel : BaseViewModelResult<LocationDto>
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

                OnSearchAdressesAsync(value);
            }
        }

        public MvxObservableCollection<LocationDto> FoundAdresses { get; private set; }

        private MvxAsyncCommand<LocationDto> _addressSelectedCommand;
        public IMvxAsyncCommand<LocationDto> AddressSelectedCommand
        {
            get
            {
                return _addressSelectedCommand ?? (_addressSelectedCommand = new MvxAsyncCommand<LocationDto>(DoAddressSelectedCommandAsync));
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

            FoundAdresses = new MvxObservableCollection<LocationDto>();
        }

        private async void OnSearchAdressesAsync(string text)
        {
            FoundAdresses.ReplaceWith(await _geocodingService.GetLocationsAsync(text));
        }

        private Task DoAddressSelectedCommandAsync(LocationDto param)
        {
            return _navigationService.Close(this, param);
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
