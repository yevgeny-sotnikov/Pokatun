using System;
using System.Globalization;
using CoreGraphics;
using MvvmCross.Converters;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Converters
{
    public sealed class BorderEditTextValidationConverter : MvxValueConverter<bool, CGColor>
    {
        protected override CGColor Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            UIColor res = value ? ColorPalette.FaileValidationColor : ColorPalette.BorderColor;

            return res.CGColor;
        }
    }
}
