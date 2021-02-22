using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Bids;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Menu
{
    public sealed class HotelMenuViewModel : BaseViewModel<HotelShortInfoDto>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IPhotosService _photosService;
        private readonly IMvxNavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly IMemoryCache _memoryCache;
        private readonly IHotelsService _hotelsService;
        private readonly IBidsService _bidsService;
        private readonly IHotelNumbersService _hotelNumbersService;
        private HotelShortInfoDto _parameter;

        public override string Title => _parameter.HotelName;

        public string Placeholder => Title == null ? null : Title[0].ToString();

        public string Subtitle => _parameter.ProfileNotCompleted ? Strings.CompleteYourProfile : _parameter.Address;

        public bool ProfileNotCompleted => _parameter.ProfileNotCompleted;

        public int HotelNumbersAmount => _parameter == null ? 0 : _parameter.HotelNumbersAmount;

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

        private MvxAsyncCommand _bidsCommand;
        public IMvxAsyncCommand BidsCommand
        {
            get
            {
                return _bidsCommand ?? (_bidsCommand = new MvxAsyncCommand(DoBidsCommandAsync));
            }
        }

        private MvxAsyncCommand _hotelNumbersCommand;
        public IMvxAsyncCommand HotelNumbersCommand
        {
            get
            {
                return _hotelNumbersCommand ?? (_hotelNumbersCommand = new MvxAsyncCommand(DoHotelNumbersCommandAsync));
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
            IUserDialogs userDialogs,
            IPhotosService photosService,
            IMvxNavigationService navigationService,
            ISecureStorage secureStorage,
            INetworkRequestExecutor networkRequestExecutor,
            IMemoryCache memoryCache,
            IHotelsService hotelsService,
            IBidsService bidsService,
            IHotelNumbersService hotelNumbersService)
        {
            _userDialogs = userDialogs;
            _photosService = photosService;
            _navigationService = navigationService;
            _secureStorage = secureStorage;
            _networkRequestExecutor = networkRequestExecutor;
            _memoryCache = memoryCache;
            _hotelsService = hotelsService;
            _bidsService = bidsService;
            _hotelNumbersService = hotelNumbersService;
        }

        public override void Prepare(HotelShortInfoDto parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            _parameter = parameter;

            RaisePropertyChanged(nameof(Title));
            RaisePropertyChanged(nameof(Placeholder));
            RaisePropertyChanged(nameof(Subtitle));
            RaisePropertyChanged(nameof(ProfileNotCompleted));
            RaisePropertyChanged(nameof(HotelNumbersAmount));

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

                if (hotel == null)
                    return;

                _parameter = new HotelShortInfoDto
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
                    RaisePropertyChanged(nameof(Placeholder)),
                    RaisePropertyChanged(nameof(Subtitle)),
                    RaisePropertyChanged(nameof(ProfileNotCompleted))
                );
            }
            else
            {
                await _navigationService.Navigate<ShowHotelProfileViewModel, HotelDto, object>(responce.Data);

                HotelShortInfoDto dto = _memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);

                if (dto == null)
                {
                    return;
                }

                _parameter = dto;

                if (_parameter.PhotoName != null)
                {
                    PhotoStream = ct => _photosService.GetAsync(_parameter.PhotoName);
                }
                else PhotoStream = null;

                await Task.WhenAll(RaisePropertyChanged(nameof(Title)), RaisePropertyChanged(nameof(Placeholder)), RaisePropertyChanged(nameof(Subtitle)));
            }
        }

        private async Task DoBidsCommandAsync()
        {
            if (ProfileNotCompleted)
            {
                await _userDialogs.AlertAsync(Strings.FillProfileMessage, Strings.Message);
                return;
            }

            ServerResponce<List<HotelNumberDto>> responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelNumbersService.GetAllAsync(true),
                new HashSet<string>()
            );

            if (responce == null)
                return;

            await _navigationService.Navigate<BidsListViewModel, List<HotelNumberDto>>(responce.Data);
        }

        private async Task DoHotelNumbersCommandAsync()
        {
            if (ProfileNotCompleted)
            {
                await _userDialogs.AlertAsync(Strings.FillProfileMessage, Strings.Message);
                return;
            }

            ServerResponce<List<HotelNumberDto>> responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelNumbersService.GetAllAsync(),
                new HashSet<string>()
            );

            if (responce == null)
                return;

            await _navigationService.Navigate<HotelNumbersListViewModel, List<HotelNumberDto>>(responce.Data);
        }


        private Task DoExitCommandAsync()
        {
            _secureStorage.RemoveAll();

            return _navigationService.Navigate<ChoiseUserRoleViewModel>();
        }
    }
}
