using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using MvvmValidation;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public class EditBidViewModel : BaseViewModel<EditBidParameter, bool>
    {
        private readonly IMemoryCache _memoryCache;

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

        public bool IsPriceInvalid => CheckInvalid(nameof(Price));

        public bool IsDiscountInvalid => CheckInvalid(nameof(Discount));

        public EditBidViewModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

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

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

    }
}
