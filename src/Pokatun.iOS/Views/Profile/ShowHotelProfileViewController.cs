using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Profile;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxChildPresentation]
    public partial class ShowHotelProfileViewController : BaseViewController<ShowHotelProfileViewModel>
    {
        public ShowHotelProfileViewController() : base(nameof(ShowHotelProfileViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            _personalDataTab.Text = Strings.PersonalData;
            _hotelInfoTab.Text = Strings.HotelInfo;
        }
    }
}

