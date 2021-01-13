using System;
using System.Collections.Generic;
using System.Globalization;
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

        private short? _number;
        public short? Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        private RoomLevel _level = RoomLevel.Standart;
        public RoomLevel Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        private byte _roomsAmount = 1;
        public byte RoomsAmount
        {
            get { return _roomsAmount; }
            set { SetProperty(ref _roomsAmount, value); }
        }

        private byte _visitorsAmount = 1;
        public byte VisitorsAmount
        {
            get { return _visitorsAmount; }
            set { SetProperty(ref _visitorsAmount, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private bool _cleaningNeeded;
        public bool CleaningNeeded
        {
            get { return _cleaningNeeded; }
            set { SetProperty(ref _cleaningNeeded, value); }
        }

        private MvxAsyncCommand _selectRoomLevelCommand;
        public IMvxAsyncCommand SelectRoomLevelCommand
        {
            get
            {
                return _selectRoomLevelCommand ?? (_selectRoomLevelCommand = new MvxAsyncCommand(DoSelectRoomLevelCommandAsync));
            }
        }

        private MvxAsyncCommand _promptRoomsAmountCommand;
        public IMvxAsyncCommand PromptRoomsAmountCommand
        {
            get
            {
                return _promptRoomsAmountCommand ?? (_promptRoomsAmountCommand = new MvxAsyncCommand(DoPromptRoomsAmountCommandAsync));
            }
        }

        private MvxAsyncCommand _promptVisitorsAmountCommand;
        public IMvxAsyncCommand PromptVisitorsAmountCommand
        {
            get
            {
                return _promptVisitorsAmountCommand ?? (_promptVisitorsAmountCommand = new MvxAsyncCommand(DoPromptVisitorsAmountCommandAsync));
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

        private async Task DoPromptRoomsAmountCommandAsync()
        {
            byte amount = await CalculateAmountAsync(Strings.PromptAmoutOfRoms);

            if (amount == 0)
            {
                return;
               }

            RoomsAmount = amount;
        }

        private async Task DoPromptVisitorsAmountCommandAsync()
        {
            byte amount = await CalculateAmountAsync(Strings.PromptAmoutOfVisitors);

            if (amount == 0)
            {
                return;
            }

            VisitorsAmount = amount;
        }

        private async Task<byte> CalculateAmountAsync(string message)
        {
            PromptResult result = await _userDialogs.PromptAsync(message, cancelText: Strings.Cancel, inputType: InputType.DecimalNumber);

            if (!result.Ok)
            {
                return 0;
            }

            byte amount;
            bool success = byte.TryParse(result.Text, out amount);

            if (!success || amount == 0)
            {
                _userDialogs.Toast(Strings.IncorrectInput);
                return 0;
            }

            return amount;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
