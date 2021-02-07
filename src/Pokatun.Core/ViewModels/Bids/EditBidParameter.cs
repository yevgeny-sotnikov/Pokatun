using System;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public sealed class EditBidParameter
    {
        public HotelNumberDto HotelNumber { get; set; }

        public BidDto Bid { get; set; }
    }
}
