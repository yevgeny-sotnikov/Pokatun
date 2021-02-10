using System;
using System.Collections.Generic;
using System.Linq;
using Pokatun.API.Entities;
using Pokatun.API.Models;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public class BidsApiService : IBidsApiService
    {
        private readonly PokatunContext _context;

        public BidsApiService(PokatunContext context)
        {
            _context = context;
        }

        public void AddNew(CreateBidsDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            foreach (TimeRangeDto timeRange in value.TimeRanges)
            {
                if (timeRange.MaxDate.Date <= timeRange.MinDate.Date)
                {
                    throw new ApiException(ErrorCodes.IncorrectTimeRangesError);
                }
            }

            foreach (TimeRangeDto timeRange in value.TimeRanges)
            {
                _context.Bids.Add(new Bid
                {
                    Price = value.Price,
                    Discount = value.Discount,
                    HotelNumberId = value.HotelNumberId,
                    MinDate = timeRange.MinDate,
                    MaxDate = timeRange.MaxDate
                });
            }

            _context.SaveChanges();
        }

        public List<BidDto> GetAll(long hotelId)
        {
            return _context.HotelNumbers.Where(x => x.HotelId == hotelId).SelectMany(x => x.Bids).Select(x => new BidDto
            {
                Id  = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                MinDate = x.MinDate,
                MaxDate = x.MaxDate,
                HotelNumberId = x.HotelNumberId
            }).ToList();
        }
    }
}
