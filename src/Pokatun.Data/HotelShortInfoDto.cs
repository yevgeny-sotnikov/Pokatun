using System;
namespace Pokatun.Data
{
    public sealed class HotelShortInfoDto
    {
        public string HotelName { get; set; }

        public string PhotoName { get; set; }

        public string Address { get; set; }

        public bool ProfileNotCompleted { get; set; }

        public int HotelNumbersAmount { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

    }
}
