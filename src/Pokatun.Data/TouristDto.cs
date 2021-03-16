using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public class TouristDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(16)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(256)]
        public string PhotoName { get; set; }
    }
}
