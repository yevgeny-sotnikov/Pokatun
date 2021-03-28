using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public class UpdateBidDto
    {
        [Required]
        public long Price { get; set; }

        [Required]
        public byte Discount { get; set; }

        [Required]
        public DateTime MinDate { get; set; }

        [Required]
        public DateTime MaxDate { get; set; }
    }
}