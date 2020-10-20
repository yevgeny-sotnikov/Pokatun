using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using ToolbarX = AndroidX.AppCompat.Widget.Toolbar;

namespace Pokatun.Droid.Views.Main
{
    [MvxActivityPresentation]
    [Activity(
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
    public class MainContainerActivity : BaseActivity<MainContainerViewModel>
    {
        public ToolbarX Toolbar { get; private set; }

        public ImageView ToolbarLogo { get; private set; }

        public TextView ToolbarTitleLabel { get; private set; }

        protected override int ActivityLayoutId => Resource.Layout.activity_main_container;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Toolbar = FindViewById<ToolbarX>(Resource.Id.toolbar);
            ToolbarLogo = FindViewById<ImageView>(Resource.Id.toolbarLogo);
            ToolbarTitleLabel = FindViewById<TextView>(Resource.Id.toolbarTitleLabel);
            SetSupportActionBar(Toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();

            return true;
        }
    }
}
