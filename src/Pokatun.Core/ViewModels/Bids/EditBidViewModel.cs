using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public class EditBidViewModel : BaseViewModel<EditBidParameter, bool>
    {
        private readonly IMemoryCache _memoryCache;

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

        public EditBidViewModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            HotelInfo = _memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);
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
    }
}
