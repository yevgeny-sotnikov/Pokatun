
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
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.ForgotPassword
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
    public sealed class NewPasswordFragment : BaseFragment<NewPasswordViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_new_password;
    }
}
