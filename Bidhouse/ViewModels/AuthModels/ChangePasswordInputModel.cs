using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.AuthModels
{
    public class ChangePasswordInputModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
