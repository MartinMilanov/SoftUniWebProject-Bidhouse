using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Models
{
    public class Review
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string ReviewerId { get; set; }
        public User Reviewer { get; set; }
        public string ReviewedUserId { get; set; }
        public User ReviewedUser { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
