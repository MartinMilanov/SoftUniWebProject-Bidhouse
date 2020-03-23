using Bidhouse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.PostModels
{
    public class GetPostsInputModel
    {

        public int SkipCount { get; set; }
        [Required(ErrorMessage = "Please specify how much posts you want from the database")]
        [Range(1,100000,ErrorMessage = "You should specify a number greater than 0")]
        public int TakeCount { get; set; }
        public string SearchInput { get; set; }
        
        public string Category { get; set; }
    }
}
