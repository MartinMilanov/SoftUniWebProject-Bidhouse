using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.AuthModels
{
    public class ChangePasswordInputModel
    {
        [Required(ErrorMessage = "Please type in your current password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Please type in your new password")]
        [StringLength(20,MinimumLength =4,ErrorMessage ="Password must be between 4 and 20 characters")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please confirm your new password")]
        public string ConfirmPassword { get; set; }
    }
}
