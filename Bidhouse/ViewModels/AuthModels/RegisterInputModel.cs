using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.AuthModels
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Please provide a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please provide a password")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please provide an email")]
        public string Email { get; set; }
        [StringLength(50,MinimumLength =1,ErrorMessage ="City name should be between 1 and 50 chracters")]
        [Required(ErrorMessage = "Please provide a city of inhabitance")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please provide a job position")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Job name should be between 1 and 100 chracters")]
        public string JobPosition { get; set; }


    }
}
