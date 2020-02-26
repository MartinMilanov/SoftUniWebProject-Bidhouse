using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class UserReviewGottenViewModel
    {
        public string Id { get; set; }
        public string ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

    }
}
