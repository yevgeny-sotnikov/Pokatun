using System;
using Pokatun.Core.Models;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class HotelRegistrationSecondStepViewModel : BaseViewModel<HotelRegistrationFirstData>
    {
        private HotelRegistrationFirstData _firstData;

        public override void Prepare(HotelRegistrationFirstData parameter)
        {
            _firstData = parameter;
        }
    }
}
