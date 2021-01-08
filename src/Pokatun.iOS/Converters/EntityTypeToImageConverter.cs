using System;
using System.Globalization;
using MvvmCross.Converters;
using Pokatun.Core.Enums;
using UIKit;

namespace Pokatun.iOS.Converters
{
    public sealed class EntityTypeToImageConverter : MvxValueConverter<EntryType, UIImage>
    {
        private static readonly UIImage PhoneImage = UIImage.FromBundle("phone");
        private static readonly UIImage WebImage = UIImage.FromBundle("web");

        protected override UIImage Convert(EntryType value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == EntryType.Phone ? PhoneImage : WebImage;
        }
    }
}
