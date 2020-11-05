
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
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Droid.Views.ChoiseUserRole;
using Pokatun.Droid.Views.PreEntrance;
using Pokatun.Droid.Views.Registration;

namespace Pokatun.Droid.Views.Menu
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = false,
        EnterAnimation = Android.Resource.Animation.FadeIn,
        PopEnterAnimation = Android.Resource.Animation.FadeIn,
        ExitAnimation = Android.Resource.Animation.SlideOutRight,
        PopExitAnimation = Android.Resource.Animation.SlideOutRight
    )]
    public class HotelMenuFragment : BaseFragment<HotelMenuViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_menu;
    }
}
