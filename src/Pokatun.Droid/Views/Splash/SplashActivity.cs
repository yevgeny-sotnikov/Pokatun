
using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace Pokatun.Droid.Views.Splash
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        Label = "@string/app_name",
        Theme = "@style/AppTheme.Splash",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher_round"
    )]
    public class SplashActivity : MvxSplashScreenActivity
    {
        public SplashActivity() : base(Resource.Layout.activity_splash_screen)
        {
        }
    }
}
