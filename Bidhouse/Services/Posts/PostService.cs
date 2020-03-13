﻿using Bidhouse.Models;
using Bidhouse.ViewModels.BidModels;
using Bidhouse.ViewModels.PostModels;
using Bidhouse.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public async Task<PostDetailViewModel> GetPost(string id)
        {
            var query = await this.db.Posts
                .Include(x => x.Creator)
                .ThenInclude(x => x.ReviewsGotten)
                .Include(b => b.Bids)
                .ThenInclude(x => x.Bidder)
                .ThenInclude(x => x.ReviewsGotten)
                .FirstOrDefaultAsync(x => x.Id == id);


            var post = new PostDetailViewModel()
            {
                Name = query.Name,
                Description = query.Description,
                Location = query.Location,
                TimeDue = query.TimeDue,
                CreatedOn = query.CreatedOn,
                Status = query.Status.ToString(),
                Creator = new UserListModel
                {
                    Id = query.Creator.Id,
                    Name = query.Creator.UserName,
                    ImageUrl = query.Creator.ImageUrl,
                    Rating = query.Creator.ReviewsGotten.Count > 0 ? query.Creator.ReviewsGotten.Sum(x => x.Rating) : 0

                },
                Bids = query.Bids.Count > 0 ? query.Bids.Select(x => new BidViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,
                    Days = x.Days,
                    Status = x.StatusOfBid.ToString(),
                    CreatedOn = x.CreatedOn,
                    Bidder = new UserListModel
                    {
                        Id = x.Bidder.Id,
                        Name = x.Bidder.UserName,
                        ImageUrl = x.Bidder.ImageUrl,
                        Rating = x.Bidder.ReviewsGotten.Count > 0 ? query.Creator.ReviewsGotten.Sum(x => x.Rating) : 0
                    }
                }).ToList() : null
            };

            return post;
        }

        public async Task<string> CreatePost(CreatePostInputModel input, string id)
        {

            var post = new Post()
            {
                Name = input.Name,
                Description = input.Description,
                Location = input.Location != null ? input.Location : "",
                TimeDue = input.Time,
                Price = input.Price,
                Creator = this.db.Users.FirstOrDefault(x => x.Id == id),
                CreatedOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            };
            await this.db.Posts.AddAsync(post);
            await this.db.SaveChangesAsync();

            return post.Id;

        }

        public async Task<bool> DeletePost(string id)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);
            this.db.Posts.Remove(post);
            await this.db.SaveChangesAsync();

            return true;

        }

        public async Task<bool> IsClosed(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new Exception("Id is null");
            }
            var result = (await this.db.Posts.FirstOrDefaultAsync(x => x.Id == id)).Status == Status.Closed ? true : false;

            return result;
        }

        public async Task<ICollection<PostListViewModel>> GetPosts()
        {
            var posts = await this.db.Posts
                 .Include(x => x.Creator)
                 .Select(x => new PostListViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     CreatedOn = x.CreatedOn,
                     Price = x.Price,
                     UserId = x.CreatorId,
                     UserName = x.Creator.UserName,
                     UserImageUrl = x.Creator.ImageUrl
                 }
                ).ToListAsync();

            return posts;
        }

        public async Task<ICollection<PostListViewModel>> GetPostsById(string id)
        {
            var posts = await this.db.Posts
                 .Include(x => x.Creator)
                 .Where(x => x.CreatorId == id)
                 .Select(x => new PostListViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     CreatedOn = x.CreatedOn,
                     Price = x.Price
                 }
                ).ToListAsync();

            return posts;
        }
    }
}
