using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class LoginDto
    {
        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        [RegularExpression(DataPatterns.Password)]
        public string Password { get; set; }
    }
}
