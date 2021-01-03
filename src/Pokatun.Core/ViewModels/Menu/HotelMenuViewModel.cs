using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        private readonly IPhotosService _photosService;
        private readonly  IMvxNavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly IHotelsService _hotelsService;

        private ShortInfoDto _parameter;

        public override string Title => _parameter?.HotelName;

        public string Placeholder => Title[0].ToString();

        private Func<CancellationToken, Task<Stream>> _photoStream;
        public Func<CancellationToken, Task<Stream>> PhotoStream
        {
            get { return _photoStream; }
            set { SetProperty(ref _photoStream, value); }
        }


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

        public HotelMenuViewModel(
            IPhotosService photosService,
            IMvxNavigationService navigationService,
            ISecureStorage secureStorage,
            INetworkRequestExecutor networkRequestExecutor,
            IHotelsService hotelsService)
        {
            _photosService = photosService;
            _navigationService = navigationService;
            _secureStorage = secureStorage;
            _networkRequestExecutor = networkRequestExecutor;
            _hotelsService = hotelsService;
        }

        public override void Prepare(ShortInfoDto parameter)
        {
            _parameter = parameter;

            RaisePropertyChanged(nameof(Title));
            PhotoStream = ct => _photosService.GetAsync(parameter.PhotoName);
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
