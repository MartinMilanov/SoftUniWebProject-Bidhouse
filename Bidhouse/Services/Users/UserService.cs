using Bidhouse.Models;
using Bidhouse.ViewModels.PostModels;
using Bidhouse.ViewModels.ReviewModels;
using Bidhouse.ViewModels.UserModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;

        public UserService(ApplicationDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<UserDetailsModel> GetUser(string id)
        {


            var query = await this.userManager.Users
                .Include(x => x.ReviewsGotten)
                .ThenInclude(x => x.Reviewer)
                .Include(x => x.ReviewsSent)
                .SingleAsync(x => x.Id == id);

            var user = new UserDetailsModel()
            {
                Id = query.Id,
                Username = query.UserName,
                WorkPosition = query.WorkPosition,
                Description = query.Description,
                City = query.City,
                ImageUrl = query.ImageUrl,
                Rating = query.ReviewsGotten.Count == 0 ? 0 : query.ReviewsGotten.Sum(x => x.Rating) / query.ReviewsGotten.Count,
                ReviewsGotten = query.ReviewsGotten.Count > 0 ? query.ReviewsGotten.Select(x => new ReviewViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Rating = x.Rating,
                    ReviewerId = x.ReviewerId,
                    ReviewerName = x.Reviewer.UserName,
                    ReviewerImg = x.Reviewer.ImageUrl


                }).ToList() : new List<ReviewViewModel>()
            };

            return user;
        }

        public async Task<ICollection<UserListModel>> GetUsers(GetUsersInputModel input)
        {
            var query = await this.userManager.Users.Include(x => x.Posts).Include(x => x.ReviewsGotten).ToListAsync();

            if (input.SearchInput != null && input.SearchInput != "null")
            {
                query = query.Where(x => x.UserName.ToLower().Contains(input.SearchInput.ToLower())).ToList();
                if (query == null || query.Count <= 0)
                {
                    return null;
                }
            }


            var users = query.Select(x => new UserListModel
            {
                Id = x.Id,
                Name = x.UserName,
                City = x.City,
                ImageUrl = x.ImageUrl,
                Rating = (x.ReviewsGotten.Count != 0) ? x.ReviewsGotten.Sum(r => r.Rating) / x.ReviewsGotten.Count : 0,
                NumberOfPosts = x.Posts.Count
            }).ToList();

            if (users.Count - input.StartAt <= 0)
            {
                return null;
            }
            else if (users.Count - input.StartAt < input.Count)
            {
                users = users.GetRange(input.StartAt, users.Count - input.StartAt);
            }
            else
            {

                users = users.GetRange(input.StartAt, input.Count);
            }
            return users;
        }


        public async Task<bool> UpdateUser(string id, UserUpdateModel input, string imageUrl)
        {
            var user = await this.userManager.FindByIdAsync(id);
            user.WorkPosition = input.JobPosition;
            user.City = input.City;
            user.Description = input.Description;
            user.ImageUrl = imageUrl;

            await this.userManager.UpdateAsync(user);



            return true;
        }



        public async Task<bool> UserExists(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
