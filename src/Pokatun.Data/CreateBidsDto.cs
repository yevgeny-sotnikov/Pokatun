using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public class CreateBidsDto
    {
        [Required]
        public long Price { get; set; }

        [Required]
        public byte Discount { get; set; }

        public List<TimeRangeDto> TimeRanges { get; set; }

        [Required]
        public long HotelNumberId { get; set; }
    }
}
