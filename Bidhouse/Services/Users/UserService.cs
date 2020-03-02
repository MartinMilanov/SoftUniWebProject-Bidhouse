﻿using Bidhouse.Models;
using Bidhouse.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<UserDetailsModel> GetUser(string id)
        {
            var query = await this.db.Users
                .Include(x => x.BidsReceived)
                .Include(x => x.BidsSent)
                .Include(x => x.ReviewsGotten)
                .Include(x => x.ReviewsSent)
                .Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);

            var user = new UserDetailsModel()
            {
                Id = query.Id,
                Username = query.Username,
                WorkPosition = query.WorkPosition,
                Description = query.Description,
                City = query.City,
                ImageUrl = query.ImageUrl,
                Posts = query.Posts.Select(x => new UserPostDetailViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.RoofPrice
                }).ToList(),
                ReviewsSent = query.ReviewsSent.Select(x => new UserReviewSentViewModel
                {
                    Id = x.Id,
                    Rating = x.Rating,
                    Description = x.Description,
                    ReviewedUser = x.ReviewedUser.Username
                }).ToList(),
                ReviewsGotten = query.ReviewsGotten.Select(x=> new UserReviewGottenViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Rating = x.Rating,
                    ReviewerId = x.ReviewerId,
                    ReviewerName = x.Reviewer.Username
                    
                    
                }).ToList(),
                BidsSent = query.BidsSent.Select(x=> new UserBidsViewModel
                {
                    Id = x.Id,
                    PostId = x.PostId,
                    PostName = x.Post.Name,
                    Status = (Status)Enum.Parse(typeof(Status),x.StatusOfBid.ToString()),
                    Price = x.Price

                }).ToList()
            };

            return user;
        }

        public async Task<ICollection<UserListModel>> GetUsers(int startAt, int count)
        {
            var query = await this.db.Users.ToListAsync();


            var users = await this.db.Users.Select(x => new UserListModel
            {
                Id = x.Id,
                Name = x.Username,
                City = x.City,
                ImageUrl = x.ImageUrl,
                Rating = (x.ReviewsGotten.Count != 0) ? x.ReviewsGotten.Sum(r => r.Rating) / x.ReviewsGotten.Count : 0,
                NumberOfPosts = x.Posts.Count
            }).ToListAsync();

            // 5 3 2     trqbva da vidq dali counta mi e kolkoto 
            if (users.Count - startAt <= 0)
            {
                return null;
            }
            else if (users.Count - startAt < count)
            {
                users = users.GetRange(startAt, users.Count - startAt);
            }
            else
            {

                users = users.GetRange(startAt, count);
            }
            return users;
        }

        public async Task<bool> UpdateUser(string id,UserUpdateModel input,string imageUrl)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.WorkPosition = input.JobPosition;
            user.City = input.City;
            user.Description = input.Description;
            user.ImageUrl = imageUrl;

           this.db.Users.Update(user);
           await this.db.SaveChangesAsync();

            return true;
        }

       

        public async Task<bool> UserExists(string id)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
