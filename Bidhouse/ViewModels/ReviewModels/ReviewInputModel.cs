using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.ReviewModels
{
    public class ReviewInputModel
    {
        [Required(ErrorMessage = "Please provide the reviewed user's id")]
        public string ReviewedUserId { get; set; }
        [Required(ErrorMessage = "Please add a rating to your review")]
        [Range(1,6,ErrorMessage = "The review rating can't be 0 or greater than 6")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Please add a description to your review")]
        [MaxLength(270,ErrorMessage = "Review description cannot be longer than 270 characters")]
        public string Description { get; set; }
    }
}
