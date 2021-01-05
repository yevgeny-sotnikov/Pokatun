
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

            TabHost.Setup();

            TabHost.AddTab(CreateTab("personalData", Resource.Id.personalDataTab, Resource.Layout.personal_data_tab, Strings.PersonalData));
            TabHost.AddTab(CreateTab("hotelInfo", Resource.Id.hotelInfoTab, Resource.Layout.hotel_info_tab, Strings.HotelInfo));

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarPhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();
            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.EditCommand).OneTime();

            set.Bind(_hotelNameLabel).To(vm => vm.HotelName).TwoWay();
            set.Bind(_fullCompanyNameLabel).To(vm => vm.FullCompanyName).TwoWay();
            set.Bind(_emailLabel).To(vm => vm.Email).TwoWay();
            set.Bind(_bankCardOrIbanLabel).To(vm => vm.BankCardOrIban).TwoWay();
            set.Bind(_bankNameLabel).To(vm => vm.BankName).TwoWay();
            set.Bind(_usreouLabel).To(vm => vm.USREOU).TwoWay();

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
