using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class TimeRangeDto
    {
        [Required]
        public DateTime MinDate { get; set; }

        [Required]
        public DateTime MaxDate { get; set; }
    }
}
