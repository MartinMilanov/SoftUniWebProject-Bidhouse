using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidhouse.Models
{
    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}