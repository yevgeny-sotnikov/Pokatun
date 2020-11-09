using System;
using CoreGraphics;
using UIKit;

namespace Pokatun.iOS.Styles
{
    public static class ViewStyles
    {

        public static void ApplyBorderedEditTextStyle(this UITextField textField)
        {
            textField.SetHeight(40);
            textField.BorderStyle = UITextBorderStyle.None;
            textField.Layer.CornerRadius = 19;
            textField.Layer.BorderWidth = 1;
            textField.Layer.BorderColor = ColorPalette.BorderColor.CGColor;
            textField.TextAlignment = UITextAlignment.Center;
            textField.EditingDidBegin += OnEditingDidBegin;
            textField.EditingDidEnd += OnEditingDidEnd;
            textField.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            textField.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyBigButtonStyle(this UIButton button)
        {
            button.ClipsToBounds = true;
            button.Font = Fonts.HelveticaNeueCyrLightGigantic;
            button.SetHeight(75);
            button.Layer.CornerRadius = 37;
            button.SetBackgroundImage(CreateBackgroundImage(ColorPalette.ButtonDefault), UIControlState.Normal);
            button.SetBackgroundImage(CreateBackgroundImage(ColorPalette.ButtonDisabled), UIControlState.Disabled);

            button.SetTitleColor(ColorPalette.ButtonTextDefault, UIControlState.Normal);
            button.SetTitleColor(ColorPalette.ButtonTextDisabled, UIControlState.Disabled);
            button.SetTitleColor(ColorPalette.ButtonTextHighlgted, UIControlState.Highlighted);

            button.BackgroundColor = ColorPalette.PrimaryLight;
        }

        public static void ApplySubGiganticLabelStyle(this UILabel label)
        {
            label.Font = Fonts.HelveticaNeueCyrLightSubGigantic;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyLargeLabelStyle(this UILabel label)
        {
            label.Font = Fonts.HelveticaNeueCyrLightLarge;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ResetStyles(this UITextField textField)
        {
            textField.EditingDidBegin -= OnEditingDidBegin;
            textField.EditingDidEnd -= OnEditingDidEnd;
        }

        // https://stackoverflow.com/questions/14523348/how-to-change-the-background-color-of-a-uibutton-while-its-highlighted
        private static UIImage CreateBackgroundImage(UIColor color)
        {
            var rect = new CGRect(x: 0.0, y: 0.0, width: 1.0, height: 1.0);
            UIGraphics.BeginImageContext(rect.Size);
            CGContext context = UIGraphics.GetCurrentContext();

            context?.SetFillColor(color.CGColor);
            context?.FillRect(rect);

            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }

        private static void SetHeight(this UIView view, int height)
        {
            view.AddConstraint(NSLayoutConstraint.Create(view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, height));
        }

        private static void OnEditingDidBegin(object sender, EventArgs e)
        {
            UITextField textField = (UITextField)sender;

            textField.TextColor = ColorPalette.PrimaryText;
            textField.Layer.BorderWidth = 2;
            textField.Layer.BorderColor = ColorPalette.PrimaryLight.CGColor;
        }

        private static void OnEditingDidEnd(object sender, EventArgs e)
        {
            UITextField textField = (UITextField)sender;

            textField.Layer.BorderWidth = 1;
            textField.Layer.BorderColor = ColorPalette.BorderColor.CGColor;
        }
    }
}
