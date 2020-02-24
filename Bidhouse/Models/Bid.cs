using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidhouse.Models
{
    public class Bid
    {
        public Bid()
        {
            this.Id = Guid.NewGuid().ToString();
            this.StatusOfBid = Status.Waiting;
        }
        public string Id { get; set; }
        public string PostId { get; set; }
        public string BidderId { get; set; }

        public string ReceiverId { get; set; }

        public Post Post { get; set; }
        public User Bidder { get; set; }
        public User Receiver { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Status StatusOfBid { get; set; }
    }
}