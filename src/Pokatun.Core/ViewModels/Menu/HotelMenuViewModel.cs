using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Executors;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Menu
{
    public sealed class HotelMenuViewModel : BaseViewModel<ShortInfoDto>
    {
        private readonly  IMvxNavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly IHotelsService _hotelsService;

        private ShortInfoDto _parameter;

        private MvxAsyncCommand _profileCommand;
        public IMvxAsyncCommand ProfileCommand
        {
            get
            {
                return _profileCommand ?? (_profileCommand = new MvxAsyncCommand(DoProfileCommandAsync));
            }
        }

        private MvxAsyncCommand _exitCommand;
        public IMvxAsyncCommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new MvxAsyncCommand(DoExitCommandAsync));
            }
        }

        public HotelMenuViewModel(IMvxNavigationService navigationService, ISecureStorage secureStorage, INetworkRequestExecutor networkRequestExecutor, IHotelsService hotelsService)
        {
            _navigationService = navigationService;
            _secureStorage = secureStorage;
            _networkRequestExecutor = networkRequestExecutor;
            _hotelsService = hotelsService;
        }

        public override void Prepare(ShortInfoDto parameter)
        {
            _parameter = parameter;
        }

        private async Task DoProfileCommandAsync()
        {
            long hotelId = long.Parse(await _secureStorage.GetAsync(Constants.Keys.AccountId));

            ServerResponce<HotelDto> responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelsService.GetAsync(hotelId),
                new HashSet<string> { ErrorCodes.AccountDoesNotExistError }
            );

            if (responce == null) return;

            if (_parameter == null || _parameter.ProfileNotCompleted)
            {
                await _navigationService.Navigate<EditHotelProfileViewModel, HotelDto, bool>(responce.Data);
            }
            else
            {
                await _navigationService.Navigate<ShowHotelProfileViewModel, HotelDto>(responce.Data);
            }
        }

        private Task DoExitCommandAsync()
        {
            _secureStorage.RemoveAll();

            return _navigationService.Navigate<ChoiseUserRoleViewModel>();
        }
    }
}
