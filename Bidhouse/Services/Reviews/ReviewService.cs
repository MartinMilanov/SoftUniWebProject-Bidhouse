using Bidhouse.Models;
using Bidhouse.Services.Users;
using Bidhouse.ViewModels.ReviewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;

        public ReviewService(ApplicationDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }
        public async Task<ReviewViewModel> PostReview(ReviewInputModel input, string reviewerId)
        {

            var reviewer = await db.Users.FirstOrDefaultAsync(x => x.Id == reviewerId);

            var isUserReal = await this.userService.UserExists(input.ReviewedUserId);

            if (isUserReal == false)
            {
                return null;
            }
            var reviewedUser = await this.db.Users
                .Include(x => x.ReviewsGotten)
                .FirstOrDefaultAsync(x => x.Id == input.ReviewedUserId);

            if (reviewedUser.ReviewsGotten.FirstOrDefault(x => x.ReviewerId == reviewer.Id) != null)
            {
                return null;
            }

            var review = new Review()
            {
                Rating = input.Rating,
                Description = input.Description,
                Reviewer = reviewer,
                ReviewedUser = reviewedUser
            };

            await this.db.Reviews.AddAsync(review);
            await this.db.SaveChangesAsync();

            var result = new ReviewViewModel()
            {
                Id = review.Id,
                Description = review.Description,
                Rating = review.Rating,

                ReviewerId = reviewer.Id,
                ReviewerImg = reviewer.ImageUrl,
                ReviewerName = reviewer.UserName

            };

            return result;




        }
    }
}
