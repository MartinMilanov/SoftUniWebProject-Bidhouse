using Bidhouse.Models;
using Bidhouse.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.BidModels
{
    public class BidListViewModel
    {
        public ICollection<BidViewModel> BidsSent { get; set; }
        public ICollection<BidViewModel> BidsReceived { get; set; }
    }
}
