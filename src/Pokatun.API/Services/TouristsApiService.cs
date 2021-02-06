using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public TouristDto GetById(long id)
        {
            if (id <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Tourist tourist = _context.Tourists.Include(x => x.Account).FirstOrDefault(x => x.Id == id);

            if (tourist == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            return new TouristDto
            {
                FullName = tourist.FullName,
                PhoneNumber = tourist.PhoneNumber,
                Email = tourist.Account.Email,
                PhotoName = tourist.Account.PhotoName
            };
        }

        public TouristShortInfoDto GetShortInfo(long id)
        {
            if (id <= 0)
            {
                throw new ApiException(ErrorCodes.IncorrectIdError);
            }

            Tourist tourist = _context.Tourists.Include(x => x.Account).SingleOrDefault(t => t.Id == id);

            if (tourist == null)
            {
                throw new ApiException(ErrorCodes.AccountDoesNotExistError);
            }

            return new TouristShortInfoDto
            {
                Fullname = tourist.FullName,
                PhotoName = tourist.Account.PhotoName
            };
        }
    }
}
