using Bidhouse.Services.Reviews;
using Bidhouse.ViewModels.ReviewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bidhouse.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController:ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async Task<ActionResult<ReviewViewModel>> PostReview([FromBody]ReviewInputModel input)
        {

            var reviewerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (reviewerId == input.ReviewedUserId)
            {
                return this.BadRequest("Cannot review yourself");
            }

            var result = await reviewService.PostReview(input, reviewerId);

            if (result == null)
            {
                return this.BadRequest("Something went wrong");
            }

            return result;

        }
    }
}
