using Bidhouse.Models;
using Bidhouse.ViewModels.BidModels;
using Bidhouse.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.PostModels
{
    public class PostDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime TimeDue { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserListModel Creator { get; set; }
        public string Status { get; set; }
        public ICollection<BidListViewModel> Bids { get; set; }

    }
}
