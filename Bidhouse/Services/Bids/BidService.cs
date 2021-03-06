﻿using Bidhouse.Models;
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
        
        public async Task<bool> ApproveBid(string bidId, string postId)
        {
            var bid = await this.db.Bids.FirstOrDefaultAsync(x => x.Id == bidId);
            if (bid == null)
            {
                return false;
            }
            if (bid.StatusOfBid == Status.Waiting)
            {
                bid.StatusOfBid = Status.Approved;

                var postUpdated = await this.db.Posts.Include(x => x.Bids).FirstOrDefaultAsync(x => x.Id == postId);
                //elow line is waht i changed
                await this.db.Bids.Include(x => x.Post)
                    .Where(x => x.PostId == postId && x.Id != bidId)
                    .ForEachAsync(x => x.StatusOfBid = Status.Declined);

                postUpdated.Status = Status.Closed;

                //I'll change this line
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

        public async Task<BidListViewModel> GetBids(string id)
        {
            var query = await this.db.Users
                .Include(x => x.BidsReceived)
                .ThenInclude(x => x.Post)
                .Include(x=>x.BidsReceived)
                .ThenInclude(x=>x.Bidder)
                .Include(x=>x.BidsSent)
                .ThenInclude(x => x.Post)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var result = new BidListViewModel
            {
                BidsReceived = query.BidsReceived.Select(x => new BidViewModel
                {
                    Id = x.Id,
                    Status = x.StatusOfBid.ToString(),
                    Days = x.Days,
                    Price = x.Price,
                    Bidder = new UserListModel
                    {
                        Id = x.Bidder.Id,
                        City = x.Bidder.City,
                        ImageUrl = x.Bidder.ImageUrl,
                        Name = x.Bidder.UserName,
                        Rating = 0
                    },
                    CreatedOn = x.CreatedOn,
                    PostId = x.PostId,
                    PostName = x.Post.Name,
                    Description = x.Description
                }).ToList(),
                BidsSent = query.BidsSent.Select(x => new BidViewModel
                {
                    Id = x.Id,
                    Status = x.StatusOfBid.ToString(),
                    Days = x.Days,
                    Price = x.Price,
                    Bidder = new UserListModel
                    {
                        Id = query.Id,
                        City = query.City,
                        ImageUrl = query.ImageUrl,
                        Name = query.UserName,
                        Rating = 0
                    },
                    CreatedOn = x.CreatedOn,
                    PostId = x.PostId,
                    PostName = x.Post.Name,
                    Description = x.Description
                }).ToList()
            };

            return result;
        }

        public async Task<bool> HasBid(string postId, string bidderId)
        {
            var post = await this.db.Posts
                .Include(x => x.Bids)
                .ThenInclude(x => x.Bidder)
                .FirstOrDefaultAsync(x => x.Id == postId);

            var result = post.Bids.FirstOrDefault(x => x.BidderId == bidderId) == null ? false : true;

            return result;
        }

        public async Task<BidViewModel> PlaceBid(BidInputModel input, string bidderId)
        {
            var bid = new Bid()
            {
                Description = input.Description,
                Price = input.Price,
                Days = input.Days,
                CreatedOn = DateTime.Now
            };

            bid.Bidder = await this.db.Users
            .Include(x => x.ReviewsGotten)
            .FirstOrDefaultAsync(x => x.Id == bidderId);

            bid.Receiver = await this.db.Users
                .FirstOrDefaultAsync(x => x.Id == input.ReceiverId);


            bid.Post = await this.db.Posts
                .FirstOrDefaultAsync(x => x.Id == input.PostId);

            await this.db.Bids.AddAsync(bid);
            await this.db.SaveChangesAsync();


            var result = new BidViewModel()
            {
                Id = bid.Id,
                Description = bid.Description,
                Price = bid.Price,
                Days = bid.Days,
                CreatedOn = bid.CreatedOn,
                Bidder = new UserListModel
                {
                    Id = bid.Bidder.Id,
                    Name = bid.Bidder.UserName,
                    ImageUrl = bid.Bidder.ImageUrl,
                    Rating = bid.Bidder.ReviewsGotten.Count > 0 ? bid.Bidder.ReviewsGotten.Sum(x => x.Rating) : 0
                }
            };

            return result;
        }
    }
}
