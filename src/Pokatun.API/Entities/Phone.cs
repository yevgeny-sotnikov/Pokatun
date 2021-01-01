using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokatun.API.Entities
{
    [Table(nameof(Phone))]
    public sealed class Phone
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Phone]
        [MaxLength(16)]
        public string Number { get; set; }

        public long HotelId { get; set; }

        public Hotel Hotel { get; set; }

    }
}
