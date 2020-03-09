using Bidhouse.Models;
using Bidhouse.ViewModels.BidModels;
using Bidhouse.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Bids
{
    public class BidService : IBidService
    {
        private readonly ApplicationDbContext db;

        public BidService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> ApproveBid(string bidId,string postId)
        {
            var bid = await this.db.Bids.FirstOrDefaultAsync(x => x.Id == bidId);
            if (bid == null)
            {
                return false;
            }
            if (bid.StatusOfBid == Status.Waiting)
            {
                bid.StatusOfBid = Status.Approved;
                var postUpdated = await this.db.Posts.FirstOrDefaultAsync(x => x.Id == postId);
                postUpdated.Status = Status.Closed;
                
                this.db.Bids.Update(bid);
                this.db.Posts.Update(postUpdated);

                await this.db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }


            
        }

        public async Task<bool> HasBid(string postId,string bidderId)
        {
            var post = await this.db.Posts
                .Include(x=>x.Bids)
                .ThenInclude(x=>x.Bidder)
                .FirstOrDefaultAsync(x => x.Id == postId);

            var result = post.Bids.FirstOrDefault(x => x.BidderId == bidderId) == null ? false : true;

            return result;
        }

        public async Task<BidListViewModel> PlaceBid(BidInputModel input, string bidderId)
        {
            var bid = new Bid()
            {
                Description = input.Description,
                Price = input.Price,
                Days = input.Days,
                CreatedOn = DateTime.Now
            };

            bid.Bidder = await this.db.Users
            .Include(x=>x.ReviewsGotten)
            .FirstOrDefaultAsync(x => x.Id == bidderId);

            bid.Receiver = await this.db.Users
                .FirstOrDefaultAsync(x => x.Id == input.ReceiverId);


            bid.Post = await this.db.Posts
                .FirstOrDefaultAsync(x => x.Id == input.PostId);

            await this.db.Bids.AddAsync(bid);
            await this.db.SaveChangesAsync();

            
            var result = new BidListViewModel()
            {
                Id = bid.Id,
                Description = bid.Description,
                Price = bid.Price,
                Days = bid.Days,
                CreatedOn = bid.CreatedOn,
                Bidder = new UserListModel
                {
                    Id = bid.Bidder.Id,
                    Name = bid.Bidder.Username,
                    ImageUrl = bid.Bidder.ImageUrl,
                    Rating = bid.Bidder.ReviewsGotten.Count > 0 ? bid.Bidder.ReviewsGotten.Sum(x => x.Rating) : 0
                }
            };

            return result;
        }
    }
}
