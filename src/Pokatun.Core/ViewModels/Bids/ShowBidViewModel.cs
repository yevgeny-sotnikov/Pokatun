using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public class ShowBidViewModel : BaseViewModel<EditBidParameter>
    {
        public override string Title => string.Format("№ {0}", HotelNumber.Number);

        public string Subtitle => HotelInfo.HotelName;

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

        public int PriceWithDiscount => (int)(Bid.Price / 100 * (100 - Bid.Discount));

        public string TimeRange => string.Format("{0:dd.MM}-{1:dd.MM}", Bid.MinDate, Bid.MaxDate);

        private HotelShortInfoDto _hotelInfo;
        public HotelShortInfoDto HotelInfo
        {
            get { return _hotelInfo; }
            set { SetProperty(ref _hotelInfo, value); }
        }

        private BidDto _bid;
        public BidDto Bid
        {
            get { return _bid; }
            set { SetProperty(ref _bid, value); }
        }

        public ShowBidViewModel(IMemoryCache memoryCache)
        {
            HotelInfo = memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);
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

            Bid = parameter.Bid;
            HotelNumber = parameter.HotelNumber;

            RaisePropertyChanged(nameof(NutritionInfo));
            RaisePropertyChanged(nameof(PriceWithDiscount));
            RaisePropertyChanged(nameof(TimeRange));
        }
    }
}
