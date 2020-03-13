using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.AuthModels
{
    public class ChangePasswordInputModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [StringLength(20,MinimumLength =4,ErrorMessage ="Password must be between 4 and 20 characters")]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
