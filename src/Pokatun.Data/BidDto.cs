using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class BidDto
    {
        [Required]
        public long Price { get; set; }

        [Required]
        public byte Discount { get; set; }

        [Required]
        public DateTime MinDate { get; set; }

        [Required]
        public DateTime MaxDate { get; set; }

        [Required]
        public long HotelNumberId { get; set; }
    }
}
