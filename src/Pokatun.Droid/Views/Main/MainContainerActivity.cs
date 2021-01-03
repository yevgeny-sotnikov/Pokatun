using System.Collections.Generic;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using FFImageLoading.Cross;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using Xamarin.Essentials;
using ToolbarX = AndroidX.AppCompat.Widget.Toolbar;

namespace Pokatun.Droid.Views.Main
{
    [MvxActivityPresentation]
    [Activity(
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)
    ]
    public class MainContainerActivity : BaseActivity<MainContainerViewModel>
    {
        public ToolbarX Toolbar { get; private set; }

        public ImageView ToolbarLogo { get; private set; }

        public TextView ToolbarTitleLabel { get; private set; }

        public ImageButton ToolbarAddPhotoButton { get; private set; }

        public ImageButton ToolbarRightButton { get; private set; }

        public MvxCachedImageView ToolbarPhotoView { get; private set; }

        public TextView ToolbarPhotoPlaceholderLabel { get; private set; }

        public FrameLayout ToolbarPhotoContainer { get; private set; }

        public View ToolbarLeftSpaceView { get; private set; }

        protected override int ActivityLayoutId => Resource.Layout.activity_main_container;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AppCenter.Start("c79dad39-68a6-4d34-b71a-c242aafe1193", typeof(Analytics), typeof(Crashes));

            Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle

            Toolbar = FindViewById<ToolbarX>(Resource.Id.toolbar);
            ToolbarLogo = FindViewById<ImageView>(Resource.Id.toolbarLogo);
            ToolbarTitleLabel = FindViewById<TextView>(Resource.Id.toolbarTitleLabel);
            ToolbarAddPhotoButton = FindViewById<ImageButton>(Resource.Id.toolbarAddPhotoButton);
            ToolbarRightButton = FindViewById<ImageButton>(Resource.Id.toolbarRightButton);
            ToolbarPhotoView = FindViewById<MvxCachedImageView>(Resource.Id.toolbarPhotoView);
            ToolbarPhotoPlaceholderLabel = FindViewById<TextView>(Resource.Id.toolbarPhotoPlaceholderLabel);
            ToolbarPhotoContainer = FindViewById<FrameLayout>(Resource.Id.toolbarPhotoContainer);
            ToolbarLeftSpaceView = FindViewById<View>(Resource.Id.toolbarLeftSpaceView);

            SetSupportActionBar(Toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            ToolbarPhotoView.Transformations = new List<ITransformation> { new CircleTransformation() };
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();

            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
