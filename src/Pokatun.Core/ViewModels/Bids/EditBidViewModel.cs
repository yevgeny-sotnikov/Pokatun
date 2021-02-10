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
    public class EditBidViewModel : BaseViewModel<EditBidParameter, bool>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserDialogs _userDialogs;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly IBidsService _bidsService;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;


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

        public EditBidViewModel(
            IMvxNavigationService navigationService,
            IMemoryCache memoryCache,
            IUserDialogs userDialogs,
            INetworkRequestExecutor networkRequestExecutor,
            IBidsService bidsService)
        {
            _navigationService = navigationService;
            _memoryCache = memoryCache;
            _userDialogs = userDialogs;
            _networkRequestExecutor = networkRequestExecutor;
            _bidsService = bidsService;

            TimeRanges = new MvxObservableCollection<ButtonItemViewModel>();

            HotelInfo = _memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Price), () => RuleResult.Assert(_viewInEditMode || Price != null, Strings.PriceDidntSetted));
            _validator.AddRule(nameof(Discount), () => RuleResult.Assert(_viewInEditMode || Discount != null && Discount >= Constants.MinimalDiscount, Strings.DiscountDidntSetted));
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
            RaisePropertyChanged(nameof(NutritionInfo));
        }

        private void DoAddTimeRangeCommand()
        {
            TimeRanges.Add(new ButtonItemViewModel(DeleteTimeRangeCommand, _userDialogs));
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

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

                return;
            }

            ServerResponce responce;

            //if (_hotelNumberIdWhichExists == null)
            //{
                responce = await _networkRequestExecutor.MakeRequestAsync(() =>
                    _bidsService.AddNewAsync(
                        HotelNumber.Id,
                        Price.Value,
                        Discount.Value,
                        TimeRanges
                            .Where(x => x.MinDate != null && x.MaxDate != null)
                            .Select(x => new TimeRangeDto { MinDate = x.MinDate.Value, MaxDate = x.MaxDate.Value })
                    ),
                    new HashSet<string> { ErrorCodes.HotelNumberAllreadyExistsError }
                );
            //}
            //else
            //{
            //    responce = await _networkRequestExecutor.MakeRequestAsync(() =>
            //        _hotelNumbersService.UpdateExistsAsync(
            //            _hotelNumberIdWhichExists.Value,
            //            Number.Value,
            //            Level,
            //            RoomsAmount,
            //            VisitorsAmount,
            //            Description,
            //            CleaningNeeded,
            //            NutritionNeeded,
            //            BreakfastIncluded = NutritionNeeded && BreakfastIncluded,
            //            DinnerIncluded = NutritionNeeded && DinnerIncluded,
            //            SupperIncluded = NutritionNeeded && SupperIncluded
            //        ),
            //        new HashSet<string> { ErrorCodes.HotelNumberDoesntExistError, ErrorCodes.HotelNumberAllreadyExistsError }
            //    );
            //}

            if (responce == null)
                return;

            await _navigationService.Close(this, true);
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

    }
}
