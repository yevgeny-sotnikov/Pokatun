using System;
using System.Globalization;
using MvvmCross.Converters;
using Pokatun.iOS.Styles;
using UIKit;

namespace Pokatun.iOS.Converters
{
    public sealed class EditTextValidationConverter : MvxValueConverter<bool, UIColor>
    {
        protected override UIColor Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? ColorPalette.FaileValidationColor : ColorPalette.PrimaryText;
        }
    }
}
