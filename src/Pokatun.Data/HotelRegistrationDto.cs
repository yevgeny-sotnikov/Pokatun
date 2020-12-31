using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public class HotelRegistrationDto : HotelDto
    {
        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        [RegularExpression(DataPatterns.Password)]
        public string Password { get; set; }
    }
}
