using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Pokatun.Core.Converters
{
    public sealed class TimeConverter : MvxValueConverter<TimeSpan?, string>
    {
        protected override string Convert(TimeSpan? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return parameter.ToString();
            }

            return value.Value.ToString("hh\\:mm");
        }
    }
}
