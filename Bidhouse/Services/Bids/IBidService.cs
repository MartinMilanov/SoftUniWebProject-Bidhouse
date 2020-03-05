using Bidhouse.Models;
using Bidhouse.ViewModels.BidModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Bids
{
    public interface IBidService
    {
        public Task<BidListViewModel> PlaceBid(BidInputModel input,string bidderId);

        public Task<bool> HasBid(string postId,string bidderId);
    }
}
