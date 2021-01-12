using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class EditHotelNumberViewModel : BaseViewModel
    {
        private static readonly IDictionary<string, RoomLevel> RoomLevelsConversions = new Dictionary<string, RoomLevel>
        {
            { Strings.Econom, RoomLevel.Econom },
            { Strings.Standart, RoomLevel.Standart },
            { Strings.Lux, RoomLevel.Lux }
        };

        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;

        public override string Title => Strings.NewHotelNumber;

        private short? _roomNumber;
        public short? RoomNumber
        {
            get { return _roomNumber; }
            set { SetProperty(ref _roomNumber, value); }
        }

        private RoomLevel _level = RoomLevel.Standart;
        public RoomLevel Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        private MvxAsyncCommand _selectRoomLevelCommand;
        public IMvxAsyncCommand SelectRoomLevelCommand
        {
            get
            {
                return _selectRoomLevelCommand ?? (_selectRoomLevelCommand = new MvxAsyncCommand(DoSelectRoomLevelCommandAsync));
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

        public EditHotelNumberViewModel(IUserDialogs userDialogs, IMvxNavigationService navigationService)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;
        }

        private async Task DoSelectRoomLevelCommandAsync()
        {
            string result = await _userDialogs.ActionSheetAsync(
                Strings.SelectRoomCategory, Strings.Cancel,
                "", null,
                Strings.Econom, Strings.Standart, Strings.Lux
            );

            if (!RoomLevelsConversions.ContainsKey(result))
            {
                return;
            }

            Level = RoomLevelsConversions[result];
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
