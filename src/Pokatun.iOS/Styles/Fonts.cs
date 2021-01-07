using System;
using UIKit;
using Xamarin.iOS;

namespace Pokatun.iOS.Styles
{
    public static class Fonts
    {
        #region FontNames

        private const string HelveticaNeueCyrLight = "helveticaneuecyr-light";
        private const string HelveticaNeueCyrBold = "helveticaneuecyr-bold";

        #endregion

        #region FontSizes

        private static readonly nfloat SmallSize = 12; //TODO: setup later
        private static readonly nfloat MediumSize = 12;
        private static readonly nfloat LargeSize = 15;
        private static readonly nfloat ExtraLargeSize = 17;
        private static readonly nfloat SubGiganticSize = 20;
        private static readonly nfloat GiganticSize = 23;

        #endregion

        #region Fonts

        public static readonly UIFont HelveticaNeueCyrLightLightSmall = UIFont.FromName(HelveticaNeueCyrLight, CalcFontMultiplier(SmallSize));
        public static readonly UIFont HelveticaNeueCyrLightMedium = UIFont.FromName(HelveticaNeueCyrLight, CalcFontMultiplier(MediumSize));
        public static readonly UIFont HelveticaNeueCyrLightLarge = UIFont.FromName(HelveticaNeueCyrLight, CalcFontMultiplier(LargeSize));
        public static readonly UIFont HelveticaNeueCyrLightExtraLarge = UIFont.FromName(HelveticaNeueCyrLight, CalcFontMultiplier(ExtraLargeSize));
        public static readonly UIFont HelveticaNeueCyrLightSubGigantic = UIFont.FromName(HelveticaNeueCyrLight, CalcFontMultiplier(SubGiganticSize));
        public static readonly UIFont HelveticaNeueCyrLightGigantic = UIFont.FromName(HelveticaNeueCyrLight, CalcFontMultiplier(GiganticSize));

        public static readonly UIFont HelveticaNeueCyrBoldSmall = UIFont.FromName(HelveticaNeueCyrBold, CalcFontMultiplier(SmallSize));
        public static readonly UIFont HelveticaNeueCyrBoldMedium = UIFont.FromName(HelveticaNeueCyrBold, CalcFontMultiplier(MediumSize));
        public static readonly UIFont HelveticaNeueCyrBoldLarge = UIFont.FromName(HelveticaNeueCyrBold, CalcFontMultiplier(LargeSize));
        public static readonly UIFont HelveticaNeueCyrBoldExtraLarge = UIFont.FromName(HelveticaNeueCyrBold, CalcFontMultiplier(ExtraLargeSize));
        public static readonly UIFont HelveticaNeueCyrBoldSubGigantic = UIFont.FromName(HelveticaNeueCyrBold, CalcFontMultiplier(SubGiganticSize));
        public static readonly UIFont HelveticaNeueCyrBoldGigantic = UIFont.FromName(HelveticaNeueCyrBold, CalcFontMultiplier(GiganticSize));

        #endregion

        private static nfloat CalcFontMultiplier(nfloat size)
        {
            string str = DeviceHardware.Model;
            return size * (str.Contains("iPod") || str.Contains("iPhone 5S") || (str.Contains("iPhone SE") && !str.Contains("2")) ? 0.75f : 1);
        }
    }
}
