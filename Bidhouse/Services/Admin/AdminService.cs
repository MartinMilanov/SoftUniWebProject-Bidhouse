using Bidhouse.Models;
using Bidhouse.Services.Posts;
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
        private readonly IPostService postService;

        public AdminService(UserManager<User> userManager, ApplicationDbContext db, IPostService postService)
        {
            this.userManager = userManager;
            this.db = db;
            this.postService = postService;
        }
        public async Task<string> DeleteUser(string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return "User not found";
            }

            var posts = this.db.Posts
                .Include(x => x.Creator)
                .Include(x=>x.Reports)
                .Where(x => x.CreatorId == user.Id);


            foreach (var post in posts)
            {
                foreach (var report in post.Reports)
                {
                    this.db.Reports.Remove(report);
                }
                this.db.Posts.Remove(post);
            }
          
            var reviews = this.db.Reviews
                .Include(x => x.ReviewedUser)
                .Include(x => x.Reviewer)
                .Where(x => x.ReviewedUserId == user.Id || x.ReviewerId == user.Id);

            foreach (var review in reviews)
            {
                this.db.Reviews.Remove(review);
            }

            var bids = this.db.Bids
                .Include(x => x.Bidder)
                .Include(x => x.Receiver)
                .Include(x=>x.Post)
                .Where(x => x.BidderId == user.Id || x.ReceiverId == user.Id);



            foreach (var bid in bids)
            {
                if (bid.StatusOfBid == Status.Approved)
                {
                    var updatedPost = bid.Post;
                    updatedPost.Status = Status.InSearch;

                    this.db.Posts.Update(updatedPost);
                }
                this.db.Bids.Remove(bid);
            }


            var reports = this.db.Reports
                .Include(x => x.Reporter)
                .Where(x => x.ReporterId == user.Id);


            foreach (var report in reports)
            {
                this.db.Reports.Remove(report);
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
