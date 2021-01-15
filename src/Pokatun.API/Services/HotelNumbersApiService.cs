using System;
using System.Collections.Generic;
using System.Linq;
using Pokatun.API.Entities;
using Pokatun.API.Models;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public class HotelNumbersApiService : IHotelNumbersApiService
    {
        private readonly PokatunContext _context;

        public HotelNumbersApiService(PokatunContext context)
        {
            _context = context;
        }

        public void AddNew(long hotelId, HotelNumberDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            IList<string> errors = new List<string>();

            if (_context.HotelNumbers.Any(number => number.HotelId == hotelId && number.Number == value.Number))
            {
                errors.Add(ErrorCodes.HotelNumberAllreadyExistsError);

                throw new ApiException(errors);
            }

            HotelNumber hotelNumber = new HotelNumber
            {
                Number = value.Number,
                Level = value.Level,
                RoomsAmount = value.RoomsAmount,
                VisitorsAmount = value.VisitorsAmount,
                Description = value.Description,
                CleaningNeeded = value.CleaningNeeded,
                NutritionNeeded = value.NutritionNeeded,
                BreakfastIncluded = value.BreakfastIncluded,
                DinnerIncluded = value.DinnerIncluded,
                SupperIncluded = value.SupperIncluded,
                Price = value.Price,

                HotelId = hotelId
            };

            _context.HotelNumbers.Add(hotelNumber);
            _context.SaveChanges();

        }

        public List<HotelNumberDto> GetAll(long hotelId)
        {
            return _context.HotelNumbers.Where(number => number.HotelId == hotelId).Select(value => new HotelNumberDto
            {
                Number = value.Number,
                Level = value.Level,
                RoomsAmount = value.RoomsAmount,
                VisitorsAmount = value.VisitorsAmount,
                Description = value.Description,
                CleaningNeeded = value.CleaningNeeded,
                NutritionNeeded = value.NutritionNeeded,
                BreakfastIncluded = value.BreakfastIncluded,
                DinnerIncluded = value.DinnerIncluded,
                SupperIncluded = value.SupperIncluded,
                Price = value.Price,
            }).ToList();
        }
    }
}
