using System;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Droid.Views.PreEntrance;

namespace Pokatun.Droid
{
    public static class Utils
    {
        public static MvxFragmentPresentationAttribute CreatePopBackStackAttribute()
        {
            return new MvxFragmentPresentationAttribute
            {
                ActivityHostViewModelType = typeof(MainContainerViewModel),
                FragmentContentId = Resource.Id.content_frame,
                AddToBackStack = true,
                EnterAnimation = Android.Resource.Animation.SlideInLeft,
                PopEnterAnimation = Android.Resource.Animation.SlideInLeft,
                ExitAnimation = Android.Resource.Animation.FadeOut,
                PopExitAnimation = Android.Resource.Animation.FadeOut,
                PopBackStackImmediateName = typeof(PreEntranceFragment).FragmentJavaName(),
                PopBackStackImmediateFlag = MvxPopBackStack.Inclusive
            };
        }
    }
}
