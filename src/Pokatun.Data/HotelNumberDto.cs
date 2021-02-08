using System;
using System.ComponentModel.DataAnnotations;

namespace Pokatun.Data
{
    public sealed class HotelNumberDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public short Number { get; set; }

        [Required]
        public RoomLevel Level { get; set; }

        [Required]
        public byte RoomsAmount { get; set; }

        [Required]
        public byte VisitorsAmount { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public bool CleaningNeeded { get; set; }

        [Required]
        public bool NutritionNeeded { get; set; }

        [Required]
        public bool BreakfastIncluded { get; set; }

        [Required]
        public bool DinnerIncluded { get; set; }

        [Required]
        public bool SupperIncluded { get; set; }
    }
}
