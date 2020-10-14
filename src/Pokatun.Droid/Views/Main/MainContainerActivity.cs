
using Android.App;
using Android.Content.PM;
using Android.Views;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.Main
{
    [Activity(
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
    public class MainContainerActivity : BaseActivity<MainContainerViewModel>
    {
        protected override int ActivityLayoutId => Resource.Layout.activity_main_container;
    }
}
