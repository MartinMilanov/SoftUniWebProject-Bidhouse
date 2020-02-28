using Bidhouse.Models;
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
        public ICollection<UserReviewGottenViewModel> ReviewsGotten { get; set; }
        public ICollection<UserReviewSentViewModel> ReviewsSent { get; set; }
        public ICollection<UserPostDetailViewModel> Posts { get; set; }
    }
}
