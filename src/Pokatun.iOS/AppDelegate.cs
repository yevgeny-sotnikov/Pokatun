using CoreGraphics;
using Foundation;
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
            bool result = base.FinishedLaunching(application, options);

            if (result)
            {
                //UILabel.Appearance.Font = Fonts.HelveticaNeueCyrLightLarge;

                UIButton.Appearance.SetBackgroundImage(Image(ColorPalette.ButtonDefault), UIControlState.Normal);
                UIButton.Appearance.SetBackgroundImage(Image(ColorPalette.ButtonDisabled), UIControlState.Disabled);
                
                UIButton.Appearance.SetTitleColor(ColorPalette.ButtonTextDefault, UIControlState.Normal);
                UIButton.Appearance.SetTitleColor(ColorPalette.ButtonTextDisabled, UIControlState.Disabled);
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
