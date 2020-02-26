using Bidhouse.Models;
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

        public Task<ICollection<UserDetailsModel>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUser(string id,UserUpdateModel input)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.WorkPosition = input.JobPosition;
            user.City = input.City;
            user.Description = input.Description;

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
