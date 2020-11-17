using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Menu;
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

            _myBidsItem.Text = Strings.MyBids;
            _myHotelNumbersItem.Text = Strings.MyHotelNumbers;
            _hotelRatingItem.Text = Strings.HotelRating;
            _profileItem.Text = Strings.Profile;
            _conditionsAndLoyaltyProgramItem.Text = Strings.ConditionsAndLoyaltyProgram;
            _securityItem.Text = Strings.Security;
            _exitItem.Text = Strings.Exit;

            _myBidsItem.Image = UIImage.FromBundle("applic");
            _myHotelNumbersItem.Image = UIImage.FromBundle("room");
            _hotelRatingItem.Image = UIImage.FromBundle("rating");
            _profileItem.Image = UIImage.FromBundle("profile");
            _conditionsAndLoyaltyProgramItem.Image = UIImage.FromBundle("loyaty");
            _securityItem.Image = UIImage.FromBundle("password");
            _exitItem.Image = UIImage.FromBundle("logout");
        }
    }
}

