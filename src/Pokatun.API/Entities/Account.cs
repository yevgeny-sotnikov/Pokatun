using System;
using System.ComponentModel.DataAnnotations;
using Pokatun.Data;

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

        [MaxLength(256)]
        public string PhotoName { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; }

        [MaxLength(DataPatterns.VerificationCodeLenght)]
        public string ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
