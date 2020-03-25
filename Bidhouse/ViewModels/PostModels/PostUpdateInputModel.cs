using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.PostModels
{
    public class PostUpdateInputModel
    {
        [Required(ErrorMessage = "The post id is missing !")]
        public string PostId { get; set; }
        [Required(ErrorMessage = "Please place a description")]
        [StringLength(270,ErrorMessage = "Your description should be longer than 10 and shorter than 270 characters !",MinimumLength = 10)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please specify a location")]
        [StringLength(50, ErrorMessage = "Your location should be longer than 10 and shorter than 50 characters !", MinimumLength = 10)]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please specify a price ! ")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please specify expected date !")]
        public DateTime Date { get; set; }


    }
}
