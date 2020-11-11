using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class ResetPasswordRequest
    {
        [Required]
        [MinLength(DataPatterns.VerificationCodeLenght)]
        [MaxLength(DataPatterns.VerificationCodeLenght)]
        public string Token { get; set; }

        [Required]
        [MaxLength(32)]
        [RegularExpression(DataPatterns.Password)]
        public string Password { get; set; }
    }
}
