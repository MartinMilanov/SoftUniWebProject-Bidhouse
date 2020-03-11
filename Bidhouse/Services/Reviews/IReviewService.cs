using Bidhouse.ViewModels.ReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Reviews
{
    public interface IReviewService
    {
        public Task<ReviewViewModel> PostReview(ReviewInputModel input,string reviewerId);
    }
}
