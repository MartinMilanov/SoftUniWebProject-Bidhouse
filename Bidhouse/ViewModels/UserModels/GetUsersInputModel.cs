using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class GetUsersInputModel
    {
         [Required]
        public int StartAt { get; set; }
        [Required]
        public int Count { get; set; }
        public string SearchInput { get; set; }

    }
}
