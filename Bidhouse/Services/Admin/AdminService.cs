using Bidhouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext db;

        public AdminService(UserManager<User> userManager,ApplicationDbContext db )
        {
            this.userManager = userManager;
            this.db = db;
        }
        public async Task<string> DeleteUser(string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return "User not found";
            }

            var reviews = this.db.Reviews
                .Include(x => x.ReviewedUser)
                .Include(x => x.Reviewer)
                .Where(x => x.ReviewedUserId == user.Id || x.ReviewerId == user.Id);

            var bids = this.db.Bids
                .Include(x=>x.Bidder)
                .Include(x=>x.Receiver)
                .Where(x => x.BidderId == user.Id || x.ReceiverId == user.Id);
            
            var posts = this.db.Posts
                .Include(x => x.Creator)
                .Where(x => x.CreatorId == user.Id);

            foreach (var post in posts)
            {
                this.db.Posts.Remove(post);
            }

            foreach (var bid in bids)
            {
                this.db.Bids.Remove(bid);
            }

            foreach (var review in reviews)
            {
                this.db.Reviews.Remove(review);
            }

            await this.db.SaveChangesAsync();

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return user.Id;
            }
            else
            {
                return "Could not delete user";
            }
        }

        public async Task<string> MakeAdmin(string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return "User not found";
            }

            var result = (await this.userManager.GetRolesAsync(user)).Any(x => x == "Admin");
            if (result == true)
            {
                return "User is already admin";
            }

            await this.userManager.AddToRoleAsync(user, "Admin");

            return user.Id;

        }
    }
}
