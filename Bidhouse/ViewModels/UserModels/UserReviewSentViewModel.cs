using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class UserReviewSentViewModel
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string ReviewedUser { get; set; }
    }
}
