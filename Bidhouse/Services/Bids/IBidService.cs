﻿using Bidhouse.Models;
using Bidhouse.ViewModels.BidModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Bids
{
    public interface IBidService
    {
        public Task<BidListViewModel> GetBids(string id);
        public Task<BidViewModel> PlaceBid(BidInputModel input,string bidderId);

        public Task<bool> HasBid(string postId,string bidderId);

        public Task<bool> ApproveBid(string bidId,string postId);
    }
}
