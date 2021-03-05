using System;
using CoreGraphics;
using Pokatun.iOS.Controls;
using UIKit;

namespace Pokatun.iOS.Styles
{
    public static class ViewStyles
    {
        public static void Cornerize(this UIView view, nfloat radius)
        {
            view.ClipsToBounds = true;
            view.Layer.CornerRadius = radius;
            view.Layer.MasksToBounds = true;
        }

        public static void ApplyBorderViewStyle(this BorderView borderView)
        {
            borderView.BorderColor = ColorPalette.BorderColor;
            borderView.HighlightedColor = ColorPalette.FailValidationColor;
            borderView.SelectionColor = ColorPalette.PrimaryLight;
        }

        public static void ApplyBorderedEditTextStyle(this BorderedTextField textField)
        {
            textField.HighlightedColor = ColorPalette.FailValidationColor;
            textField.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyBorderedButtonStyle(this BorderedButton button)
        {
            button.TextColor = ColorPalette.PrimaryText;
            button.HighlightedColor = ColorPalette.FailValidationColor;
        }

        public static void ApplyShadowedButtonStyle(this UIButton button)
        {
            button.SetBackgroundImage(CreateBackgroundImage(ColorPalette.ShadowButtonColor), UIControlState.Normal);
        }

        public static void ApplyBigButtonStyle(this UIButton button)
        {
            button.ClipsToBounds = true;
            button.Font = Fonts.HelveticaNeueCyrLightGigantic;
            button.SetHeight(75);
            button.Cornerize(37);
            button.SetBackgroundImage(CreateBackgroundImage(ColorPalette.ButtonDefault), UIControlState.Normal);
            button.SetBackgroundImage(CreateBackgroundImage(ColorPalette.ButtonDisabled), UIControlState.Disabled);

            button.SetTitleColor(ColorPalette.ButtonTextDefault, UIControlState.Normal);
            button.SetTitleColor(ColorPalette.ButtonTextDisabled, UIControlState.Disabled);
            button.SetTitleColor(ColorPalette.ButtonTextHighlgted, UIControlState.Highlighted);

            button.BackgroundColor = ColorPalette.PrimaryLight;
        }

        public static void ApplyCheckBoxStyle(this UICheckBox checkbox)
        {
            checkbox.ContentEdgeInsets = new UIEdgeInsets(0, 8, 0, 0);
            checkbox.ImageEdgeInsets = new UIEdgeInsets(0, -8, 0, 0);
            checkbox.Font = Fonts.HelveticaNeueCyrLightMedium;
            checkbox.SetTitleColor(ColorPalette.AdditionalText, UIControlState.Normal);
            checkbox.SetImage(UIImage.FromBundle("ch_blank"), UIControlState.Normal);
            checkbox.SetImage(UIImage.FromBundle("ch_active"), UIControlState.Selected);
        }

        public static void ApplySwitchStyle(this UISwitch uiswitch)
        {
            uiswitch.OnTintColor = ColorPalette.PrimaryText;
        }

        public static void ApplyInfoLabelStyle(this UILabel label)
        {
            label.Font = Fonts.HelveticaNeueCyrLightLarge;
            label.TextColor = ColorPalette.PrimaryText;
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
        }

        public static void ApplyAdditionalInfoLabelStyle(this UILabel label)
        {
            label.Font = Fonts.HelveticaNeueCyrLightMedium;
            label.TextColor = ColorPalette.BorderColor;
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
        }

        public static void ApplyMediumLabelStyle(this UILabel label)
        {
            label.Font = Fonts.HelveticaNeueCyrLightMedium;
            label.TextColor = ColorPalette.PrimaryText;
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
        }

        public static void ApplyTitleLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrBoldLarge;
            label.TextColor = ColorPalette.SecondaryText;
        }

        public static void ApplySubGiganticLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrLightSubGigantic;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyLargeLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrLightLarge;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyLargeBoldLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrBoldLarge;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyMediumBoldLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrBoldMedium;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyExtraLargeLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrLightExtraLarge;
            label.TextColor = ColorPalette.PrimaryText;
        }

        public static void ApplyInfoTabLabelStyle(this UILabel label)
        {
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.Font = Fonts.HelveticaNeueCyrBoldLarge;
            label.TextColor = ColorPalette.TabText;
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
    }
}
