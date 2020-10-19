using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.ViewModels.Registration;
using UIKit;

namespace Pokatun.iOS.Views.Registration
{
    [MvxChildPresentation]
    public sealed partial class HotelRegistrationFirstStepViewController
        : BaseViewController<HotelRegistrationFirstStepViewModel>
    {
        public HotelRegistrationFirstStepViewController() : base(nameof(HotelRegistrationFirstStepViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}

