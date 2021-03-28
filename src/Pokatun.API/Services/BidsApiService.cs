using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

                if (value.TimeRanges.Any(x => x != timeRange && x.MinDate < timeRange.MaxDate && timeRange.MinDate < x.MaxDate))
                {
                    throw new ApiException(ErrorCodes.NewTimeRangesOverlappingError);
                }

                if (_context.Bids.Any(x => x.HotelNumberId == value.HotelNumberId && x.MinDate < timeRange.MaxDate && timeRange.MinDate < x.MaxDate))
                {
                    throw new ApiException(ErrorCodes.OccupiedTimeRangesError);
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

        public void UpdateExists(long bidId, UpdateBidDto value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.MaxDate <= value.MinDate)
            {
                throw new ApiException(ErrorCodes.IncorrectTimeRangesError);
            }

            Bid bid = _context.Bids.First(b => b.Id == bidId);

            if (_context.Bids.Any(x => x.HotelNumberId == bid.HotelNumberId && bid.Id != x.Id && x.MinDate < value.MaxDate && value.MinDate < x.MaxDate))
            {
                throw new ApiException(ErrorCodes.OccupiedTimeRangesError);
            }

            bid.Price = value.Price;
            bid.Discount = value.Discount;
            bid.MinDate = value.MinDate;
            bid.MaxDate = value.MaxDate;

            _context.Update(bid);
            _context.SaveChanges();
        }
    }
}
