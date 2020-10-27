using System;
namespace Pokatun.Data
{
    public sealed class Hotel
    {
        public string HotelName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullCompanyName { get; set; }

        public ulong BankCard { get; set; }

        public string IBAN { get; set; }

        public string BankName { get; set; }

        public uint USREOU { get; set; }
    }
}
