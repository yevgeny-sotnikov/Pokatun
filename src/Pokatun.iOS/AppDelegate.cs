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
                UILabel.Appearance.Font = Fonts.HelveticaNeueCyrLightLarge;


                UIButton.AppearanceWhenContainedIn(typeof(UINavigationBar)).SetBackgroundImage(null, UIControlState.Normal);
                UIButton.AppearanceWhenContainedIn(typeof(UINavigationBar)).BackgroundColor = null;
            }

            return result;
        }

        // https://stackoverflow.com/questions/14523348/how-to-change-the-background-color-of-a-uibutton-while-its-highlighted
        private UIImage Image(UIColor color)
        {
            var rect = new CGRect(x: 0.0, y: 0.0, width: 1.0, height: 1.0);
            UIGraphics.BeginImageContext(rect.Size);
            var context = UIGraphics.GetCurrentContext();

            context?.SetFillColor(color.CGColor);
            context?.FillRect(rect);

            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }
    }
}
