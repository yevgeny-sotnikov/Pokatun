using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
