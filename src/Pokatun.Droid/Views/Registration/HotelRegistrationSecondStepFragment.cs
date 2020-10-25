using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Registration;

namespace Pokatun.Droid.Views.Registration
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
    public sealed class HotelRegistrationSecondStepFragment : BaseFragment<HotelRegistrationSecondStepViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_registration_second_step;
    }
}
