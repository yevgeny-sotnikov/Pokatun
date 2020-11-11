using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public class ValidateResetTokenRequest
    {
        [Required]
        [MinLength(DataPatterns.VerificationCodeLenght)]
        [MaxLength(DataPatterns.VerificationCodeLenght)]
        public string Token { get; set; }
    }
}
