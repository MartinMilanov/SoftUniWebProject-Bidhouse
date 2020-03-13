using Bidhouse.Models;
using Bidhouse.ViewModels.PostModels;
using Bidhouse.ViewModels.ReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class UserDetailsModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string WorkPosition { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<UserBidsViewModel> BidsSent { get; set; }
        public ICollection<UserBidsViewModel> BidsReceived { get; set; }
        public ICollection<ReviewViewModel> ReviewsGotten { get; set; }
        public ICollection<PostListViewModel> Posts { get; set; }
    }
}
