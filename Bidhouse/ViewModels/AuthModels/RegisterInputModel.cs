using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.AuthModels
{
    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 characters")]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [StringLength(25,MinimumLength =1,ErrorMessage ="City name should be between 1 and 25 chracters")]
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Job name should be between 1 and 15 chracters")]
        public string JobPosition { get; set; }


    }
}
