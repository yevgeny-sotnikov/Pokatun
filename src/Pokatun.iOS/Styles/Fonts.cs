using System;
using UIKit;

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
        private static readonly nfloat MediumSize = 14; //TODO: setup later
        private static readonly nfloat LargeSize = 15;
        private static readonly nfloat ExtraLargeSize = 17;
        private static readonly nfloat SubGiganticSize = 20;
        private static readonly nfloat GiganticSize = 23;

        #endregion

        #region Fonts

        public static readonly UIFont HelveticaNeueCyrLightLightSmall = UIFont.FromName(HelveticaNeueCyrLight, SmallSize);
        public static readonly UIFont HelveticaNeueCyrLightMedium = UIFont.FromName(HelveticaNeueCyrLight, MediumSize);
        public static readonly UIFont HelveticaNeueCyrLightLarge = UIFont.FromName(HelveticaNeueCyrLight, LargeSize);
        public static readonly UIFont HelveticaNeueCyrLightExtraLarge = UIFont.FromName(HelveticaNeueCyrLight, ExtraLargeSize);
        public static readonly UIFont HelveticaNeueCyrLightSubGigantic = UIFont.FromName(HelveticaNeueCyrLight, SubGiganticSize);
        public static readonly UIFont HelveticaNeueCyrLightGigantic = UIFont.FromName(HelveticaNeueCyrLight, GiganticSize);

        public static readonly UIFont HelveticaNeueCyrBoldSmall = UIFont.FromName(HelveticaNeueCyrBold, SmallSize);
        public static readonly UIFont HelveticaNeueCyrBoldMedium = UIFont.FromName(HelveticaNeueCyrBold, MediumSize);
        public static readonly UIFont HelveticaNeueCyrBoldLarge = UIFont.FromName(HelveticaNeueCyrBold, LargeSize);
        public static readonly UIFont HelveticaNeueCyrBoldExtraLarge = UIFont.FromName(HelveticaNeueCyrBold, ExtraLargeSize);
        public static readonly UIFont HelveticaNeueCyrBoldSubGigantic = UIFont.FromName(HelveticaNeueCyrBold, SubGiganticSize);
        public static readonly UIFont HelveticaNeueCyrBoldGigantic = UIFont.FromName(HelveticaNeueCyrBold, GiganticSize);

        #endregion
    }
}
