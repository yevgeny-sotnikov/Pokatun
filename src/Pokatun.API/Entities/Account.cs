using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.API.Entities
{
    public sealed class Account
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; }
    }
}
