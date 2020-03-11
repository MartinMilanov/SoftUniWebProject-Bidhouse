using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.ReviewModels
{
    public class ReviewViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public string ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerImg { get; set; }
    }
}
