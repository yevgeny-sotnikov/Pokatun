using System;
using System.Globalization;
using MvvmCross.Converters;
using Pokatun.Core.Enums;

namespace Pokatun.Droid.Converters
{
    public sealed class EntityTypeToImageConverter : MvxValueConverter<EntryType, int>
    {
        protected override int Convert(EntryType value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == EntryType.Phone ? Resource.Drawable.phone : Resource.Drawable.web;
        }
    }
}
