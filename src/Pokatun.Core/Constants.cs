using System;
namespace Pokatun.Core
{
    public static class Constants
    {
        #if DEBUG

        public const string iOSDebugIP = "localhost";

        public const string AndroidDebugIP = "10.0.2.2";

        public const string BaseUrl = "https://{0}:5001";

#else

        public const string BaseUrl = "http://yevgenysotnikov-001-site1.etempurl.com";

#endif

        public const string VisicomKey = "977190b058daecfff65cba75f24b2c50";

        public const string InvalidValueExceptionMessage = "Invalid value";

        public const string CirclePattern = "[⬤]+?";

        public const string PhonePattern = @"^(\+)\d{12,}$";

        public const byte MinimalDiscount = 20;

        public static class Keys
        {
            public const string Token = "Token";
            public const string TokenExpirationTime = "ExpirationTime";
            public const string AccountId = "AccountId";

            public const string UserRole = "UserRole";
            public const string ShortHotelInfo = "ShortHotelInfo";
            public const string ShortTouristInfo = "ShortTouristInfo";
        }
    }
}
