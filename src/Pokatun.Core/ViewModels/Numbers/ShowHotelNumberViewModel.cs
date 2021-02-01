using System;
using Microsoft.Extensions.Caching.Memory;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class ShowHotelNumberViewModel : BaseViewModel<HotelNumberDto, HotelNumberDto>
    {
        private readonly IMemoryCache _memoryCache;

        public override string Title => string.Format(Strings.HotelNumber, HotelNumber.Number);

        public string Subtitle
        {
            get
            {
                ShortInfoDto shortInfo = _memoryCache.Get<ShortInfoDto>(Constants.Keys.ShortHotelInfo);

                return shortInfo.HotelName;
            }
        }

        private HotelNumberDto _hotelNumber;
        public HotelNumberDto HotelNumber
        {
            get { return _hotelNumber; }
            set { SetProperty(ref _hotelNumber, value); }
        }

        public ShowHotelNumberViewModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public override void Prepare(HotelNumberDto parameter)
        {
            HotelNumber = parameter;
        }
    }
}
