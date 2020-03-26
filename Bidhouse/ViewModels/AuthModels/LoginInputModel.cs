using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.AuthModels
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "You should type a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "You should type a password")]
        public string Password { get; set; }
    }
}
