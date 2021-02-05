using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.API.Entities
{
    public class Tourist
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string FullName { get; set; }

        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string PhoneNumber { get; set; }

        public long AccountId { get; set; }

        public Account Account { get; set; }
    }
}
