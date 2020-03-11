using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.ReviewModels
{
    public class ReviewInputModel
    {
        [Required]
        public string ReviewedUserId { get; set; }
        [Required]
        [Range(1,6)]
        public int Rating { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
