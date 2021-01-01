using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class PhoneDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Phone]
        [MaxLength(16)]
        public string Number { get; set; }
    }
}
