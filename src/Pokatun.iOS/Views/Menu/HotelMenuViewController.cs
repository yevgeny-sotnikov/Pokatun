using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Menu
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class HotelMenuViewController : BaseViewController<HotelMenuViewModel>
    {
        public HotelMenuViewController() : base(nameof(HotelMenuViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _cardView.ClipsToBounds = true;
            _cardView.ShadowOpacity = 1;
            _cardView.CornerRadius = 4;
            _cardView.ShadowColor = UIColor.Gray;

            _menuContainer.Cornerize(4);

            _myBidsItem.Text = Strings.MyBids;
            _myHotelNumbersItem.Text = Strings.MyHotelNumbers;
            _hotelRatingItem.Text = Strings.HotelRating;
            _profileItem.Text = Strings.Profile;
            _conditionsAndLoyaltyProgramItem.Text = Strings.ConditionsAndLoyaltyProgram;
            _securityItem.Text = Strings.Security;
            _exitItem.Text = Strings.Exit;
        }
    }
}

