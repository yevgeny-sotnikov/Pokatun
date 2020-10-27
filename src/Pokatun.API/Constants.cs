using System;
namespace Pokatun.API
{
    public static class Constants
    {
        public static class AuthOptions
        {
            public const string IsUser = "MyAuthServer"; 
            public const string Audience = "MyAuthClient"; 
            public const string Key = "mysupersecret_secretkey!123";
            public const int LIFETIME = 1;
        }
    }
}
