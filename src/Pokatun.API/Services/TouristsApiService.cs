using System;
using System.Linq;
using Pokatun.API.Entities;
using Pokatun.API.Models;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public class TouristsApiService : ITouristsApiService
    {
        private readonly PokatunContext _context;

        public TouristsApiService(PokatunContext context)
        {
            _context = context;
        }

        public TouristShortInfoDto GetShortInfo(long id)
        {
            if (id <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Tourist tourist = _context.Tourists.SingleOrDefault(t => t.Id == id);

            if (tourist == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            return new TouristShortInfoDto
            {
                Fullname = tourist.FullName
            };
        }
    }
}
