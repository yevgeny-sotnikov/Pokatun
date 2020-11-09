using System;
namespace Pokatun.Core
{
    public static class Constants
    {
        public const string iOSDebugIP = "localhost";

        public const string AndroidDebugIP = "10.0.2.2";

        public const string BaseUrl = "https://{0}:34312";

        public const string InvalidValueExceptionMessage = "Invalid value";

        public static class Keys
        {
            public const string Token = "Token";
            public const string TokenExpirationTime = "ExpirationTime";
        }
    }
}
