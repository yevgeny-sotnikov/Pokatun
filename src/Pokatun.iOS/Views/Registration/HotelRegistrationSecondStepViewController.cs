using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.Registration;
using UIKit;

namespace Pokatun.iOS.Views.Registration
{
    [MvxChildPresentation]
    public sealed partial class HotelRegistrationSecondStepViewController : BaseViewController<HotelRegistrationSecondStepViewModel>
    {
        public HotelRegistrationSecondStepViewController() : base(nameof(HotelRegistrationSecondStepViewController), null)
        {
        }
    }
}

