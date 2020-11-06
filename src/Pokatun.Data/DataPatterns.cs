using System;
namespace Pokatun.Data
{
    public static class DataPatterns
    {
        public const string Email = @"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$";

        public const string IBAN = @"^[A-Z]{2}\d\d[A-Z0-9]{15,30}$";

        public const string Phone = @"^(\+)\d{12,}$";

        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-^*#'!@$%^&(){}\[\]:;<>,\.?\/~_`+=|\\])[-A-Za-z\d^*#'!@$%^&(){}\[\]:;<>,\.?\/~_`+=|\\]{8,}$";

        public const string BankCard = @"^\d{16}$";

        public const string USREOU = @"^\d{8}$";
    }
}
