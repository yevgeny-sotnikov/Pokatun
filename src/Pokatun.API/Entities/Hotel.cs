using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pokatun.Data;

namespace Pokatun.API.Entities
{
    [Table(nameof(Hotel))]
    public sealed class Hotel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        public string HotelName { get; set; }

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

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        [MaxLength(200)]
        public string WithinTerritoryDescription { get; set; }

        [MaxLength(600)]
        public string HotelDescription { get; set; }

        [MaxLength(256)]
        public string PhotoUrl { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        public double? Longtitude { get; set; }

        public double? Latitude { get; set; }

        public ICollection<Phone> Phones { get; set; }

        public ICollection<SocialResource> SocialResources { get; set; }

        public ICollection<HotelNumber> HotelNumbers { get; set; }

        public long AccountId { get; set; }

        public Account Account { get; set; }
    }
}
