using System;
using UIKit;

namespace Pokatun.iOS.Styles
{
    public static class ColorPalette
    {
        public static readonly UIColor Primary = FromHexString("#51629a");
        public static readonly UIColor PrimaryDark = FromHexString("#51629a");
        public static readonly UIColor PrimaryLight = FromHexString("#6f7bb8");

        public static readonly UIColor Accent = FromHexString("#4c555a");
        public static readonly UIColor AccentDark = FromHexString("#242c31");
        public static readonly UIColor AccentLight = FromHexString("#788187");

        public static readonly UIColor PrimaryText = FromHexString("#000000");
        public static readonly UIColor SecondaryText = FromHexString("#ffffff");

        public static readonly UIColor ButtonDefault = FromHexString("#edcf2b");
        public static readonly UIColor ButtonDisabled = FromHexString("#e5e8ef");

        public static readonly UIColor ButtonTextDefault = FromHexString("#5d5d5d");
        public static readonly UIColor ButtonTextDisabled = UIColor.White;

        private static UIColor FromHexString(string hexValue)
        {
            var colorString = hexValue.Replace("#", "");
            float red, green, blue;

            switch (colorString.Length)
            {
                case 3: // #RGB
                    {
                        red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                        green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                        blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
                        return UIColor.FromRGB(red, green, blue);
                    }
                case 6: // #RRGGBB
                    {
                        red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                        green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                        blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                        return UIColor.FromRGB(red, green, blue);
                    }
                case 8: // #AARRGGBB
                    {
                        var alpha = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                        red = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                        green = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                        blue = Convert.ToInt32(colorString.Substring(6, 2), 16) / 255f;
                        return UIColor.FromRGBA(red, green, blue, alpha);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(hexValue), $"Color value {hexValue} is invalid. Expected format #RBG, #RRGGBB, or #AARRGGBB");
            }
        }
    }
}
