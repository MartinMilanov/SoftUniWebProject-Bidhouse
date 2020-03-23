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
        [Required]
        [StringLength(70, MinimumLength = 10, ErrorMessage = "Name length must be between 10 and 70 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(270, MinimumLength = 10, ErrorMessage = "Name length must be between 10 and 270 characters")]
        public string Description { get; set; }
        
        public string Location { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
