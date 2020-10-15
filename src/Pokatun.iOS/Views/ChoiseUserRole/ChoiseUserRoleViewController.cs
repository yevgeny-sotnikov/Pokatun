using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ChoiseUserRole;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.ChoiseUserRole
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class ChoiseUserRoleViewController : BaseViewController<ChoiseUserRoleViewModel>
    {
        protected override bool NavigationBarHidden => true;

        public ChoiseUserRoleViewController() : base(nameof(ChoiseUserRoleViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _chooseRoleLabel.Font = Fonts.HelveticaNeueCyrLightSubGigantic;
            _touristButton.Font = Fonts.HelveticaNeueCyrLightGigantic;
            _hotelButton.Font = Fonts.HelveticaNeueCyrLightGigantic;

            _chooseRoleLabel.Text = Strings.ChooseRoleText;
            _hotelDescriptionLabel.Text = Strings.HotelRoleDescriptionText;
            _touristDescriptionLabel.Text = Strings.TouristRoleDescriptionText;
            _touristButton.SetTitle(Strings.Tourist, UIControlState.Normal);
            _hotelButton.SetTitle(Strings.Hotel, UIControlState.Normal);

            _touristButton.Enabled = false;
        }
    }
}

