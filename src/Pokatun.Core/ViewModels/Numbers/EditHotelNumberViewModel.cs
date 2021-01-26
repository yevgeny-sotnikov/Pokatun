using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
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
        private readonly IHotelNumbersService _hotelNumbersService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;

        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;

        public override string Title => Strings.NewHotelNumber;

        private short? _number;
        public short? Number
        {
            get { return _number; }
            set
            {
                if (!SetProperty(ref _number, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsNumberInvalid));
            }
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
            set
            {
                if (!SetProperty(ref _description, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsDescriptionInvalid));
            }
        }

        private bool _cleaningNeeded;
        public bool CleaningNeeded
        {
            get { return _cleaningNeeded; }
            set { SetProperty(ref _cleaningNeeded, value); }
        }

        private bool _nutritionNeeded;
        public bool NutritionNeeded
        {
            get { return _nutritionNeeded; }
            set { SetProperty(ref _nutritionNeeded, value); }
        }

        private bool _breakfastIncluded;
        public bool BreakfastIncluded
        {
            get { return _breakfastIncluded; }
            set { SetProperty(ref _breakfastIncluded, value); }
        }

        private bool _dinnerIncluded;
        public bool DinnerIncluded
        {
            get { return _dinnerIncluded; }
            set { SetProperty(ref _dinnerIncluded, value); }
        }

        private bool _supperIncluded;
        public bool SupperIncluded
        {
            get { return _supperIncluded; }
            set { SetProperty(ref _supperIncluded, value); }
        }

        private long? _price;
        public long? Price
        {
            get { return _price; }
            set
            {
                if (!SetProperty(ref _price, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsPriceInvalid));
            }
        }

        public bool IsNumberInvalid => CheckInvalid(nameof(Number));

        public bool IsPriceInvalid => CheckInvalid(nameof(Price));

        public bool IsDescriptionInvalid => CheckInvalid(nameof(Description));

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

        private MvxAsyncCommand _saveChangesCommand;
        public IMvxAsyncCommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new MvxAsyncCommand(DoSaveChangesCommandAsync));
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

        public EditHotelNumberViewModel(IUserDialogs userDialogs, IMvxNavigationService navigationService, IHotelNumbersService hotelNumbersService, INetworkRequestExecutor networkRequestExecutor)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _hotelNumbersService = hotelNumbersService;
            _networkRequestExecutor = networkRequestExecutor;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Number), () => RuleResult.Assert(_viewInEditMode || Number != null, Strings.RoomNumberDidntSetted));
            _validator.AddRule(nameof(Price), () => RuleResult.Assert(_viewInEditMode || Price != null, Strings.PriceDidntSetted));
            _validator.AddRule(nameof(Description), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(Description), Strings.NeedSetupNumberDescription));
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

        private async Task DoSaveChangesCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsDescriptionInvalid)),
                RaisePropertyChanged(nameof(IsNumberInvalid)),
                RaisePropertyChanged(nameof(IsPriceInvalid))
            );

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

                return;
            }

            ServerResponce responce = await _networkRequestExecutor.MakeRequestAsync(() =>
                _hotelNumbersService.AddNewAsync(
                    Number.Value,
                    Level,
                    RoomsAmount,
                    VisitorsAmount,
                    Description,
                    CleaningNeeded,
                    NutritionNeeded,
                    BreakfastIncluded = NutritionNeeded && BreakfastIncluded,
                    DinnerIncluded = NutritionNeeded && DinnerIncluded,
                    SupperIncluded = NutritionNeeded && SupperIncluded,
                    Price.Value
                ),
                new HashSet<string> { ErrorCodes.HotelNumberAllreadyExistsError }
            );

            if (responce == null)
                return;

            await _navigationService.Close(this);
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
