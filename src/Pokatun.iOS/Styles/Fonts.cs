using System;
using UIKit;

namespace Pokatun.iOS.Styles
{
    public static class Fonts
    {
        #region FontNames

        private const string HelveticaNeueCyrLight = "";

        #endregion

        #region FontSizes

        private static readonly nfloat SmallSize = 12;
        private static readonly nfloat MediumSize = 14;
        private static readonly nfloat LargeSize = 20;
        private static readonly nfloat GiganticSize = 30;

        #endregion

        #region Fonts

        public static readonly UIFont HelveticaNeueCyrLightLightSmall = UIFont.FromName(HelveticaNeueCyrLight, SmallSize);
        public static readonly UIFont HelveticaNeueCyrLightMedium = UIFont.FromName(HelveticaNeueCyrLight, MediumSize);
        public static readonly UIFont HelveticaNeueCyrLightLarge = UIFont.FromName(HelveticaNeueCyrLight, LargeSize);
        public static readonly UIFont HelveticaNeueCyrLightGigantic = UIFont.FromName(HelveticaNeueCyrLight, GiganticSize);

        #endregion
    }
}
