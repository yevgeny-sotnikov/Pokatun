using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class SocialResourceDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Link { get; set; }
    }
}
