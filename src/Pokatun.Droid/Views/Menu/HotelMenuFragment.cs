
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
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Droid.Controls;
using Pokatun.Droid.Views.ChoiseUserRole;
using Pokatun.Droid.Views.PreEntrance;
using Pokatun.Droid.Views.Registration;

namespace Pokatun.Droid.Views.Menu
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = false,
        PopEnterAnimation = Android.Resource.Animation.FadeIn,
        ExitAnimation = Android.Resource.Animation.SlideOutRight,
        PopExitAnimation = Android.Resource.Animation.SlideOutRight
    )]
    public class HotelMenuFragment : BaseFragment<HotelMenuViewModel>
    {
        private MenuItem _myBidsItem;
        private MenuItem _myHotelNumbersItem;
        private MenuItem _hotelRatingItem;
        private MenuItem _profileItem;
        private MenuItem _conditionsAndLoyaltyProgramItem;
        private MenuItem _securityItem;
        private MenuItem _exitItem;

        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_menu;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _myBidsItem = view.FindViewById<MenuItem>(Resource.Id.myBidsItem);
            _myHotelNumbersItem = view.FindViewById<MenuItem>(Resource.Id.myHotelNumbersItem);
            _hotelRatingItem = view.FindViewById<MenuItem>(Resource.Id.hotelRatingItem);
            _profileItem = view.FindViewById<MenuItem>(Resource.Id.profileItem);
            _conditionsAndLoyaltyProgramItem = view.FindViewById<MenuItem>(Resource.Id.conditionsAndLoyaltyProgramItem);
            _securityItem = view.FindViewById<MenuItem>(Resource.Id.securityItem);
            _exitItem = view.FindViewById<MenuItem>(Resource.Id.exitItem);

            _myBidsItem.Text = Strings.MyBids;
            _myHotelNumbersItem.Text = Strings.MyHotelNumbers;
            _hotelRatingItem.Text = Strings.HotelRating;
            _profileItem.Text = Strings.Profile;
            _conditionsAndLoyaltyProgramItem.Text = Strings.ConditionsAndLoyaltyProgram;
            _securityItem.Text = Strings.Security;
            _exitItem.Text = Strings.Exit;

            _myBidsItem.SetImageResource(Resource.Drawable.applic);
            _myHotelNumbersItem.SetImageResource(Resource.Drawable.room);
            _hotelRatingItem.SetImageResource(Resource.Drawable.rating);
            _profileItem.SetImageResource(Resource.Drawable.profile);
            _conditionsAndLoyaltyProgramItem.SetImageResource(Resource.Drawable.loyaty);
            _securityItem.SetImageResource(Resource.Drawable.password);
            _exitItem.SetImageResource(Resource.Drawable.logout);


            return view;
        }
    }
}