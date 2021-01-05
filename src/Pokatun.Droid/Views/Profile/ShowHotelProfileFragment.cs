
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Profile;

namespace Pokatun.Droid.Views.Profile
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = true,
        EnterAnimation = Android.Resource.Animation.SlideInLeft,
        PopEnterAnimation = Android.Resource.Animation.SlideInLeft,
        ExitAnimation = Android.Resource.Animation.SlideOutRight,
        PopExitAnimation = Android.Resource.Animation.SlideOutRight
    )]
    public class ShowHotelProfileFragment : TabFragment<ShowHotelProfileViewModel>
    {
        private TextView _hotelNameLabel;
        private TextView _fullCompanyNameLabel;
        private TextView _emailLabel;
        private TextView _bankCardOrIbanLabel;
        private TextView _bankNameLabel;
        private TextView _usreouLabel;
        private TextView _infrastructureLabel;
        private TextView _infrastructureDecsriptionLabel;
        private TextView _aboutUsLabel;
        private TextView _hotelDecsriptionLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_show_hotel_profile;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TabHost = view.FindViewById<TabHost>(Android.Resource.Id.TabHost);

            _hotelNameLabel = view.FindViewById<TextView>(Resource.Id.hotelNameLabel);
            _fullCompanyNameLabel = view.FindViewById<TextView>(Resource.Id.fullCompanyNameLabel);
            _emailLabel = view.FindViewById<TextView>(Resource.Id.emailLabel);
            _bankCardOrIbanLabel = view.FindViewById<TextView>(Resource.Id.bankCardOrIbanLabel);
            _bankNameLabel = view.FindViewById<TextView>(Resource.Id.bankNameLabel);
            _usreouLabel = view.FindViewById<TextView>(Resource.Id.usreouLabel);
            _infrastructureLabel = view.FindViewById<TextView>(Resource.Id.infrastructureLabel);
            _infrastructureDecsriptionLabel = view.FindViewById<TextView>(Resource.Id.infrastructureDecsriptionLabel);
            _aboutUsLabel = view.FindViewById<TextView>(Resource.Id.aboutUsLabel);
            _hotelDecsriptionLabel = view.FindViewById<TextView>(Resource.Id.hotelDecsriptionLabel);

            TabHost.Setup();

            TabHost.AddTab(CreateTab("personalData", Resource.Id.personalDataTab, Resource.Layout.personal_data_tab, Strings.PersonalData));
            TabHost.AddTab(CreateTab("hotelInfo", Resource.Id.hotelInfoTab, Resource.Layout.hotel_info_tab, Strings.HotelInfo));

            _infrastructureLabel.Text = Strings.Infrastructure;
            _aboutUsLabel.Text = Strings.AboutUs;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarPhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.EditCommand).OneTime();

            set.Bind(_hotelNameLabel).To(vm => vm.HotelName).OneWay();
            set.Bind(_fullCompanyNameLabel).To(vm => vm.FullCompanyName).OneWay();
            set.Bind(_emailLabel).To(vm => vm.Email).OneWay();
            set.Bind(_bankCardOrIbanLabel).To(vm => vm.BankCardOrIban).OneWay();
            set.Bind(_bankNameLabel).To(vm => vm.BankName).OneWay();
            set.Bind(_usreouLabel).To(vm => vm.USREOU).OneWay();
            set.Bind(_infrastructureDecsriptionLabel).To(vm => vm.WithinTerritoryDescription).OneWay();
            set.Bind(_hotelDecsriptionLabel).To(vm => vm.HotelDescription).OneWay();

            set.Apply();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarLeftSpaceView.Visibility = ViewStates.Gone;
            ToolbarRightButton.SetImageResource(Resource.Drawable.edit);

            ToolbarRightButton.Visibility = ViewStates.Visible;
            ToolbarAddPhotoButton.Visibility = ViewStates.Invisible;
            ToolbarPhotoContainer.Visibility = ViewStates.Visible;
            ToolbarPhotoPlaceholderLabel.Visibility = ViewStates.Visible;
        }

    }
}
