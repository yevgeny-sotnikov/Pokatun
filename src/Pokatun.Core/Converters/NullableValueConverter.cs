using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Pokatun.Core.Converters
{
    public class NullableValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return null;

            return value;
        }
    }
}
