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
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Days { get; set; }
        
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserListModel Bidder { get; set; }
    }
}
