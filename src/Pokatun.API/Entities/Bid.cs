using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokatun.API.Entities
{
    [Table(nameof(Bid))]
    public sealed class Bid
    {
        [Key]
        public long Id { get; set; }

        public long HotelNumberId { get; set; }

        public HotelNumber HotelNumber { get; set; }

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
