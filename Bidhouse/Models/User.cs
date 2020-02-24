using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WorkPosition { get; set; }
        public string Description { get; set; }
        public ICollection<Bid> BidsSent { get; set; }
        public ICollection<Bid> BidsReceived { get; set; }
        public ICollection<Review> ReviewsGotten { get; set; }
        public ICollection<Review> ReviewsSent { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
