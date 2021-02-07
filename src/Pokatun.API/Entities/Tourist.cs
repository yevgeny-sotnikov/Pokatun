using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokatun.API.Entities
{
    [Table(nameof(Tourist))]
    public class Tourist
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string FullName { get; set; }

        [Required]
        [MaxLength(16)]
        [Phone]
        public string PhoneNumber { get; set; }

        public long AccountId { get; set; }

        public Account Account { get; set; }
    }
}
