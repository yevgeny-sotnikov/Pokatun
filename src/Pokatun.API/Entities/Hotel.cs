using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pokatun.Data;

namespace Pokatun.API.Entities
{
    [Table(nameof(Hotel))]
    public class Hotel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        public string HotelName { get; set; }

        [Required]
        [Phone]
        [MaxLength(16)]
        public string PhoneNumber { get; set; }

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

        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        public string FullCompanyName { get; set; }

        public long? BankCard { get; set; }

        [MaxLength(34)]
        [RegularExpression(DataPatterns.IBAN)]
        public string IBAN { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(1)]
        public string BankName { get; set; }

        [Required]
        public int USREOU { get; set; }
    }
}
