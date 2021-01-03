using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Menu
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class HotelMenuViewController : BaseViewController<HotelMenuViewModel>
    {
        private TitlePhotoView _titlePhotoView;

        public HotelMenuViewController() : base(nameof(HotelMenuViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _titlePhotoView = TitlePhotoView.Create();

            NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_titlePhotoView), true);

            _cardView.ClipsToBounds = true;
            _cardView.ShadowOpacity = 1;
            _cardView.CornerRadius = 4;
            _cardView.ShadowColor = UIColor.Gray;
            
            _menuContainer.Cornerize(4);

            ViewTitle.IsSubtitleHidden = false;
            _titlePhotoView.Placeholder = ViewModel.Placeholder;
            _myBidsItem.Text = Strings.MyBids;
            _myHotelNumbersItem.Text = Strings.MyHotelNumbers;
            _hotelRatingItem.Text = Strings.HotelRating;
            _profileItem.Text = Strings.Profile;
            _conditionsAndLoyaltyProgramItem.Text = Strings.ConditionsAndLoyaltyProgram;
            _securityItem.Text = Strings.Security;
            _exitItem.Text = Strings.Exit;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_titlePhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(ViewTitle).For(v => v.Subtitle).To(vm => vm.Subtitle).OneWay();
            set.Bind(ViewTitle).For(v => v.SubtitleHightlighted).To(vm => vm.SubtitleHightlighted).OneWay();

            set.Bind(_profileItem).For(nameof(MenuItem.Clicked)).To(vm => vm.ProfileCommand);
            set.Bind(_exitItem).For(nameof(MenuItem.Clicked)).To(vm => vm.ExitCommand);
            
            set.Apply();
        }
    }
}

