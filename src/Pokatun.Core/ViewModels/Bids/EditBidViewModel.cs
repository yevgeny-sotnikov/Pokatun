using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public class EditBidViewModel : BaseViewModel<EditBidParameter, BidDto[]>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly IBidsService _bidsService;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;
        private long? _bidIdWhichExists;

        private HotelNumberDto _hotelNumber;
        public HotelNumberDto HotelNumber
        {
            get { return _hotelNumber; }
            set { SetProperty(ref _hotelNumber, value); }
        }

        public string NutritionInfo
        {
            get
            {
                if (!HotelNumber.NutritionNeeded)
                    return Strings.NotIncluded;

                List<string> nutritionInfoList = new List<string>(3);

                if (HotelNumber.BreakfastIncluded)
                    nutritionInfoList.Add(Strings.Breakfast);

                if (HotelNumber.DinnerIncluded)
                    nutritionInfoList.Add(Strings.Dinner);

                if (HotelNumber.SupperIncluded)
                    nutritionInfoList.Add(Strings.Supper);

                return string.Join(", ", nutritionInfoList);
            }
        }

        private HotelShortInfoDto _hotelInfo;
        public HotelShortInfoDto HotelInfo
        {
            get { return _hotelInfo; }
            set { SetProperty(ref _hotelInfo, value); }
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

        private byte? _discount;
        public byte? Discount
        {
            get { return _discount; }
            set
            {
                if (!SetProperty(ref _discount, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsDiscountInvalid));
            }
        }

        private DateTime? _minDate;
        public DateTime? MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private DateTime? _maxDate;
        public DateTime? MaxDate
        {
            get { return _maxDate; }
            set { SetProperty(ref _maxDate, value); }
        }

        private bool? _isEditMode;
        public bool? IsEditMode
        {
            get { return _isEditMode; }
            set { SetProperty(ref _isEditMode, value); }
        }

        public MvxObservableCollection<ButtonItemViewModel> TimeRanges { get; private set; }

        public bool IsPriceInvalid => CheckInvalid(nameof(Price));

        public bool IsDiscountInvalid => CheckInvalid(nameof(Discount));

        private MvxCommand _addTimeRangeCommand;
        public IMvxCommand AddTimeRangeCommand
        {
            get
            {
                return _addTimeRangeCommand ?? (_addTimeRangeCommand = new MvxCommand(DoAddTimeRangeCommand));
            }
        }

        private MvxAsyncCommand _editTimeRangeCommand;
        public IMvxAsyncCommand EditTimeRangeCommand
        {
            get
            {
                return _editTimeRangeCommand ?? (_editTimeRangeCommand = new MvxAsyncCommand(DoEditTimeRangeCommandAsync));
            }
        }

        private MvxCommand<ButtonItemViewModel> _deleteTimeRangeCommand;
        public IMvxCommand<ButtonItemViewModel> DeleteTimeRangeCommand
        {
            get
            {
                return _deleteTimeRangeCommand ?? (_deleteTimeRangeCommand = new MvxCommand<ButtonItemViewModel>(DoDeleteTimeRangeCommand));
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

        private ButtonItemViewModel[] InvalidTimeRanges => TimeRanges.Where(p => p.MaxDate == null || p.MinDate == null || p.MaxDate <= p.MinDate).ToArray();

        private ButtonItemViewModel[] OverlappedTimeRanges
        {
            get
            {
                List<ButtonItemViewModel> overlappedItems = new List<ButtonItemViewModel>();

                foreach (ButtonItemViewModel timeRange in TimeRanges)
                {
                    if (TimeRanges.Any(x => x != timeRange && x.MinDate < timeRange.MaxDate && timeRange.MinDate < x.MaxDate))
                    {
                        overlappedItems.Add(timeRange);
                    }
                }

                return overlappedItems.ToArray();
            }
        }

        private bool IsTimeRangeInvalid(ButtonItemViewModel vm)
        {
            return vm.MaxDate == null || vm.MinDate == null || vm.MaxDate <= vm.MinDate || OverlappedTimeRanges.Contains(vm);
        }

        public EditBidViewModel(
            IMvxNavigationService navigationService,
            IMemoryCache memoryCache,
            IUserDialogs userDialogs,
            INetworkRequestExecutor networkRequestExecutor,
            IBidsService bidsService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _networkRequestExecutor = networkRequestExecutor;
            _bidsService = bidsService;

            TimeRanges = new MvxObservableCollection<ButtonItemViewModel>();

            HotelInfo = memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Price), () => RuleResult.Assert(_viewInEditMode || Price != null, Strings.PriceDidntSetted));
            _validator.AddRule(nameof(Discount), () => RuleResult.Assert(_viewInEditMode || Discount != null && Discount >= Constants.MinimalDiscount, Strings.DiscountDidntSetted));
            _validator.AddRule(nameof(InvalidTimeRanges), () => RuleResult.Assert(_viewInEditMode || !InvalidTimeRanges.Any(), Strings.InvalidTimeRangesMessage));
            _validator.AddRule(nameof(OverlappedTimeRanges), () => RuleResult.Assert(_viewInEditMode || !OverlappedTimeRanges.Any(), Strings.NewTimeRangesOverlappingError));
        }

        public override void Prepare(EditBidParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (parameter.HotelNumber == null)
            {
                throw new InvalidOperationException("Hotel number must be passed");
            }

            HotelNumber = parameter.HotelNumber;

            if (parameter.Bid != null)
            {
                _bidIdWhichExists = parameter.Bid.Id;

                Price = parameter.Bid.Price;
                Discount = parameter.Bid.Discount;

                MinDate = parameter.Bid.MinDate;
                MaxDate = parameter.Bid.MaxDate;

                IsEditMode = true;
            }
            else
            {
                IsEditMode = false;
            }

            RaisePropertyChanged(nameof(NutritionInfo));
        }

        private void DoAddTimeRangeCommand()
        {
            TimeRanges.Add(new ButtonItemViewModel(DeleteTimeRangeCommand, _userDialogs, IsTimeRangeInvalid));
        }

        private async Task DoEditTimeRangeCommandAsync()
        {
            DatePromptResult minResult = await _userDialogs.DatePromptAsync(selectedDate: MinDate);

            if (!minResult.Ok)
            {
                return;
            }

            DatePromptResult maxResult = await _userDialogs.DatePromptAsync(new DatePromptConfig
            {
                SelectedDate = minResult.Value,
                MinimumDate = MinDate == null ? null : MinDate
            });

            if (!maxResult.Ok)
            {
                return;
            }

            MinDate = minResult.Value;
            MaxDate = maxResult.Value;
        }

        private void DoDeleteTimeRangeCommand(ButtonItemViewModel obj)
        {
            TimeRanges.Remove(obj);
        }

        private async Task DoSaveChangesCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(RaisePropertyChanged(nameof(IsDiscountInvalid)), RaisePropertyChanged(nameof(IsPriceInvalid)));

            foreach (ButtonItemViewModel vm in TimeRanges)
            {
                vm.Validate();
            }

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

                return;
            }

            ServerResponce responce;

            if (_bidIdWhichExists == null)
            {
                responce = await _networkRequestExecutor.MakeRequestAsync(() =>
                    _bidsService.AddNewAsync(
                        HotelNumber.Id,
                        Price.Value,
                        Discount.Value,
                        TimeRanges.Select(x => new TimeRangeDto { MinDate = x.MinDate.Value, MaxDate = x.MaxDate.Value })
                    ),
                    new HashSet<string> { ErrorCodes.OccupiedTimeRangesError }
                );
            }
            else
            {
                responce = await _networkRequestExecutor.MakeRequestAsync(() =>
                    _bidsService.UpdateExistsAsync(
                        _bidIdWhichExists.Value,
                        Price.Value,
                        Discount.Value,
                        MinDate.Value,
                        MaxDate.Value
                    ),
                    new HashSet<string> { ErrorCodes.OccupiedTimeRangesError }
                );
            }

            if (responce == null)
                return;

            if (_bidIdWhichExists == null)
            {
                await _navigationService.Close(this, TimeRanges.Select(x => new BidDto
                {
                    HotelNumberId = HotelNumber.Id,
                    Price = Price.Value,
                    Discount = Discount.Value,
                    MinDate = x.MinDate.Value,
                    MaxDate = x.MaxDate.Value
                }).ToArray());
            }
            else
            {
                await _navigationService.Close(this, new BidDto[]
                {
                    new BidDto
                    {
                        Id = _bidIdWhichExists.Value,
                        HotelNumberId = HotelNumber.Id,
                        Price = Price.Value,
                        Discount = Discount.Value,
                        MinDate = MinDate.Value,
                        MaxDate = MaxDate.Value
                    }
                });
            }
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
