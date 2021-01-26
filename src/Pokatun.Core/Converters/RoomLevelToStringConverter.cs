using System;
using System.Collections.Generic;
using System.Globalization;
using MvvmCross.Converters;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.Converters
{
    public sealed class RoomLevelToStringConverter : MvxValueConverter<RoomLevel, string>
    {
        private static readonly IDictionary<RoomLevel, string> RoomLevelsConversions = new Dictionary<RoomLevel, string>
        {
            { RoomLevel.Econom, Strings.Econom },
            { RoomLevel.Standart, Strings.Standart },
            { RoomLevel.Lux, Strings.Lux }
        };

        protected override string Convert(RoomLevel value, Type targetType, object parameter, CultureInfo culture)
        {
            return RoomLevelsConversions[value];
        }
    }
}
