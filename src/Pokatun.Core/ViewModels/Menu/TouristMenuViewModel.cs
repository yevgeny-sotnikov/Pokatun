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
using Pokatun.Core.ViewModels.Tourist;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Menu
{
    public sealed class TouristMenuViewModel: BaseViewModel<TouristShortInfoDto>
    {
        private readonly IPhotosService _photosService;
        private readonly IMvxNavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly ITouristsService _touristsService;

        private string _title;
        public override string Title => _title;

        public string Placeholder => Title == null ? null : Title[0].ToString();

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

        public TouristMenuViewModel(
            IPhotosService photosService,
            IMvxNavigationService navigationService,
            ISecureStorage secureStorage,
            INetworkRequestExecutor networkRequestExecutor,
            ITouristsService touristsService)
        {
            _photosService = photosService;
            _navigationService = navigationService;
            _secureStorage = secureStorage;
            _networkRequestExecutor = networkRequestExecutor;
            _touristsService = touristsService;
        }

        public override void Prepare(TouristShortInfoDto parameter)
        {
            _title = parameter.Fullname;

            RaisePropertyChanged(nameof(Title));
            RaisePropertyChanged(nameof(Placeholder));

            if (parameter != null && parameter.PhotoName != null)
            {
                PhotoStream = ct => _photosService.GetAsync(parameter.PhotoName);
            }
            else PhotoStream = null;

        }

        private async Task DoProfileCommandAsync()
        {
            long touristId = long.Parse(await _secureStorage.GetAsync(Constants.Keys.AccountId));

            ServerResponce<TouristDto> responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _touristsService.GetAsync(touristId),
                new HashSet<string> { ErrorCodes.AccountDoesNotExistError }
            );

            if (responce == null)
                return;

            await _navigationService.Navigate<ShowTouristProfileViewModel, TouristDto, object>(responce.Data);

            //_parameter = _memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);
            //if (_parameter.PhotoName != null)
            //{
            //    PhotoStream = ct => _photosService.GetAsync(_parameter.PhotoName);
            //}
            //else
            //    PhotoStream = null;

            //await Task.WhenAll(RaisePropertyChanged(nameof(Title)), RaisePropertyChanged(nameof(Subtitle)));
        }

        private Task DoExitCommandAsync()
        {
            _secureStorage.RemoveAll();

            return _navigationService.Navigate<ChoiseUserRoleViewModel>();
        }
    }
}
