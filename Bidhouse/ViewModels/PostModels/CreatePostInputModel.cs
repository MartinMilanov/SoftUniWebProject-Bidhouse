using Bidhouse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.PostModels
{
    public class CreatePostInputModel
    {
        [Required(ErrorMessage = "You should provide a post name")]
        [StringLength(70, MinimumLength = 10, ErrorMessage = "Name length must be between 10 and 70 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should provide a description")]
        [StringLength(270, MinimumLength = 10, ErrorMessage = "Name length must be between 10 and 270 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage ="You should provide a location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "You should provide a budget ")]
        [Range(0,5000000000,ErrorMessage = "Your budget cannot be 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "You should choose a category")]
        public Category Category { get; set; }
        [Required(ErrorMessage = "You should choose a date of delivery")]
        public DateTime Time { get; set; }
    }
}
