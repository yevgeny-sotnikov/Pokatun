using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
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

        public string Placeholder => Title == null ? null : Title[0].ToString();

        public string Subtitle => _parameter == null || _parameter.ProfileNotCompleted ? Strings.CompleteYourProfile : "{location}";

        public bool ProfileNotCompleted => _parameter == null || _parameter.ProfileNotCompleted;

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
            RaisePropertyChanged(nameof(Subtitle));
            RaisePropertyChanged(nameof(ProfileNotCompleted));

            if (parameter != null && _parameter.PhotoName != null)
            {
                PhotoStream = ct => _photosService.GetAsync(_parameter.PhotoName);
            }
            else PhotoStream = null;
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
                HotelDto hotel = await _navigationService.Navigate<EditHotelProfileViewModel, HotelDto, HotelDto>(responce.Data);

                _parameter = new ShortInfoDto
                {
                    HotelName = hotel.HotelName,
                    PhotoName = hotel.PhotoUrl,
                    ProfileNotCompleted = (hotel.BankCard == null && hotel.IBAN == null)
                        || hotel.BankName == null
                        || hotel.CheckInTime == null
                        || hotel.CheckOutTime == null
                        || hotel.Email == null
                        || hotel.FullCompanyName == null
                        || hotel.HotelDescription == null
                        || hotel.HotelName == null
                        || hotel.PhotoUrl == null
                        || hotel.USREOU == 0
                        || hotel.WithinTerritoryDescription == null
                        || !hotel.Phones.Any()
                        || !hotel.SocialResources.Any()
                };

                if (_parameter.PhotoName != null)
                {
                    PhotoStream = ct => _photosService.GetAsync(_parameter.PhotoName);
                }
                else PhotoStream = null;

                await Task.WhenAll(
                    RaisePropertyChanged(nameof(Title)),
                    RaisePropertyChanged(nameof(Subtitle)),
                    RaisePropertyChanged(nameof(ProfileNotCompleted))
                );
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
