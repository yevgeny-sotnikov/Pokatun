using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokatun.API.Entities
{
    [Table(nameof(SocialResource))]

    public sealed class SocialResource
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Link { get; set; }

        public long HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
