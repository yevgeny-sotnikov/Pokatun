using CoreGraphics;
using Foundation;
using Hackiftekhar.IQKeyboardManager.Xamarin;
using MvvmCross.Platforms.Ios.Core;
using Pokatun.Core;
using Pokatun.iOS.Styles;
using Pokatun.iOS.Views.ChoiseUserRole;
using UIKit;

namespace Pokatun.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary options)
        {
            IQKeyboardManager.SharedManager().Enable = true;

            bool result = base.FinishedLaunching(application, options);

            if (result)
            {
                UILabel.AppearanceWhenContainedIn(typeof(UINavigationBar)).TextColor = ColorPalette.SecondaryText;
                UILabel.AppearanceWhenContainedIn(typeof(UINavigationBar)).Font = Fonts.HelveticaNeueCyrBoldLarge;

                UIButton.AppearanceWhenContainedIn(typeof(UINavigationBar)).SetBackgroundImage(null, UIControlState.Normal);
                UIButton.AppearanceWhenContainedIn(typeof(UINavigationBar)).BackgroundColor = null;
            }

            return result;
        }
    }
}
