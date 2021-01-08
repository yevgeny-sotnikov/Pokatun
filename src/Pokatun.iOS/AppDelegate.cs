using Foundation;
using Hackiftekhar.IQKeyboardManager.Xamarin;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Platforms.Ios.Core;
using Pokatun.Core;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary options)
        {
            AppCenter.Start("32d1464f-8618-4507-8ce0-a375b0215601", typeof(Analytics), typeof(Crashes));

            IQKeyboardManager.SharedManager().Enable = true;

            bool result = base.FinishedLaunching(application, options);

            if (result)
            {
                UIButton.AppearanceWhenContainedIn(typeof(UINavigationBar)).SetBackgroundImage(null, UIControlState.Normal);
                UIButton.AppearanceWhenContainedIn(typeof(UINavigationBar)).BackgroundColor = null;
            }

            return result;
        }
    }
}
