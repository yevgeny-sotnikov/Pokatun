using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Profile;
using Pokatun.iOS.Controls;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Views.Profile
{
    [MvxChildPresentation]
    public partial class ShowHotelProfileViewController : BaseViewController<ShowHotelProfileViewModel>
    {
        private TitlePhotoView _titlePhotoView;

        public ShowHotelProfileViewController() : base(nameof(ShowHotelProfileViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            // Perform any additional setup after loading the view, typically from a nib.
            
            UIBarButtonItem rightBarButtonItem = new UIBarButtonItem { Image = UIImage.FromBundle("edit") };

            _titlePhotoView = TitlePhotoView.Create();

            NavigationItem.LeftItemsSupplementBackButton = true;
            base.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_titlePhotoView), true);
            NavigationItem.SetRightBarButtonItem(rightBarButtonItem, false);

            _fullCompanyNameLabel.ApplyLargeLabelStyle();
            _hotelNameLabel.ApplyLargeLabelStyle();
            _bankCardOrIbanLabel.ApplyLargeLabelStyle();
            _bankNameLabel.ApplyLargeLabelStyle();
            _emailLabel.ApplyLargeLabelStyle();
            _hotelLocationLabel.ApplyLargeLabelStyle();
            _usreouLabel.ApplyLargeLabelStyle();
            _infrastructureLabel.ApplyAdditionalInfoLabelStyle();
            _infrastructureDecsriptionLabel.ApplyLargeLabelStyle();
            _aboutUsLabel.ApplyAdditionalInfoLabelStyle();
            _hotelDecsriptionLabel.ApplyLargeLabelStyle();

            _personalDataTab.Text = Strings.PersonalData;
            _hotelInfoTab.Text = Strings.HotelInfo;
            _infrastructureLabel.Text = Strings.Infrastructure;
            _aboutUsLabel.Text = Strings.AboutUs;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_titlePhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.EditCommand).OneTime();

            set.Bind(_hotelNameLabel).To(vm => vm.HotelName).TwoWay();
            set.Bind(_fullCompanyNameLabel).To(vm => vm.FullCompanyName).TwoWay();
            set.Bind(_emailLabel).To(vm => vm.Email).TwoWay();
            set.Bind(_bankCardOrIbanLabel).To(vm => vm.BankCardOrIban).TwoWay();
            set.Bind(_bankNameLabel).To(vm => vm.BankName).TwoWay();
            set.Bind(_usreouLabel).To(vm => vm.USREOU).TwoWay();
            set.Bind(_infrastructureDecsriptionLabel).To(vm => vm.WithinTerritoryDescription).OneWay();
            set.Bind(_hotelDecsriptionLabel).To(vm => vm.HotelDescription).OneWay();

            set.Apply();

        }
    }
}

