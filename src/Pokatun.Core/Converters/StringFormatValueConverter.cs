using System;
using MvvmCross.Converters;

namespace Pokatun.Core.Converters
{
    public sealed class StringFormatValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            return string.Format((string)parameter, value);
        }
    }
}
