using System;
namespace Pokatun.Core
{
    public static class Constants
    {
        public const string EmailPattern = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";

        public const string PhonePattern = @"^(\+)\d{12,}$";
            
        public const string PasswordPattern = @"^[-A-Za-z0-9^*#'!@$%^&(){}\[\]:;<>,\.?\/~_`+=|\\]{8,}$";

        public const string BankCardPattern = @"^\d{16}$";

        public const string UsreouPattern = @"^\d{8}$";

        public const string IbanPattern = @"^[A-Z]{2}\d\d[A-Z0-9]{15,30}$";
    }
}
