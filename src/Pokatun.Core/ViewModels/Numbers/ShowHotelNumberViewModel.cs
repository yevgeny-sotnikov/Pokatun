using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class ShowHotelNumberViewModel : BaseViewModel<HotelNumberDto, HotelNumberDto>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMvxNavigationService _navigationService;

        public override string Title => string.Format(Strings.HotelNumber, HotelNumber.Number);

        public string Subtitle
        {
            get
            {
                HotelShortInfoDto shortInfo = _memoryCache.Get<HotelShortInfoDto>(Constants.Keys.ShortHotelInfo);

                return shortInfo.HotelName;
            }
        }

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

                StringBuilder stringBuilder = new StringBuilder();

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

        private MvxAsyncCommand _editCommand;
        public IMvxAsyncCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new MvxAsyncCommand(DoEditCommandAsync));
            }
        }

        public ShowHotelNumberViewModel(IMemoryCache memoryCache, IMvxNavigationService navigationService)
        {
            _memoryCache = memoryCache;
            _navigationService = navigationService;
        }

        public override void Prepare(HotelNumberDto parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            HotelNumber = parameter;
            RaisePropertyChanged(nameof(NutritionInfo));
        }

        private async Task DoEditCommandAsync()
        {
            HotelNumberDto result = await _navigationService.Navigate<EditHotelNumberViewModel, HotelNumberDto, HotelNumberDto>(HotelNumber);

            if (result == null)
            {
                return;
            }

            HotelNumber = result;

            await RaiseAllPropertiesChanged();
        }

    }
}
