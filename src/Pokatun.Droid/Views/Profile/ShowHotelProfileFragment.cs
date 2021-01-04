
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
        protected override int FragmentLayoutId => Resource.Layout.fragment_show_hotel_profile;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TabHost = view.FindViewById<TabHost>(Android.Resource.Id.TabHost);

            TabHost.Setup();

            TabHost.AddTab(CreateTab("personalData", Resource.Id.personalDataTab, Resource.Layout.personal_data_tab, Strings.PersonalData));
            TabHost.AddTab(CreateTab("hotelInfo", Resource.Id.hotelInfoTab, Resource.Layout.hotel_info_tab, Strings.HotelInfo));

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarLeftSpaceView.Visibility = ViewStates.Gone;

            ToolbarRightButton.SetImageResource(Resource.Drawable.close);
            ToolbarRightButton.Visibility = ViewStates.Visible;
            ToolbarAddPhotoButton.Visibility = ViewStates.Invisible;
            ToolbarPhotoContainer.Visibility = ViewStates.Visible;
            ToolbarPhotoPlaceholderLabel.Visibility = ViewStates.Visible;
        }

    }
}
