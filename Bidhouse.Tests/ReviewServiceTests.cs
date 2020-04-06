using Bidhouse.Models;
using Bidhouse.Services.Reviews;
using Bidhouse.Services.Users;
using Bidhouse.Tests.Factories;
using Bidhouse.ViewModels.ReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bidhouse.Tests
{
    public class ReviewServiceTests
    {
        public ServiceFactory services { get; set; }
        public IReviewService reviewService { get; set; }
        public ReviewServiceTests()
        {
            this.services = new ServiceFactory();
            var userService = new UserService(this.services.UserManager);
            this.reviewService = new ReviewService(this.services.Context, userService);
        }
        [Fact]
        public async Task PostReviewShouldPostReview()
        {
            var reviewedUser = new User()
            {
                UserName = "ReviewedUser"
            };
            var reviewingUser = new User()
            {
                UserName = "ReviewingUser"
                
            };

           await this.services.UserManager.CreateAsync(reviewedUser,"12345678");
           await this.services.UserManager.CreateAsync(reviewingUser, "12345678");
               

            var input = new ReviewInputModel()
            {
                Description = "Review",
                Rating = 5,
                ReviewedUserId = reviewedUser.Id
            };

           var result = await this.reviewService.PostReview(input, reviewingUser.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostReviewShouldReturnNull()
        {
        

            var input = new ReviewInputModel()
            {
                Description = "Review",
                Rating = 5,
                ReviewedUserId = ""
            };

            var result = await this.reviewService.PostReview(input, "");

            Assert.Null(result);
        }

    }
}
