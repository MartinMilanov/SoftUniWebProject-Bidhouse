using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class UserUpdateModel
    {
        [MaxLength(100, ErrorMessage = "Your job position must be shorter than 100 characters")]
        public string JobPosition { get; set; }
        [MaxLength(100,ErrorMessage = "Your city must be shorter than 100 characters")]
        public string City { get; set; }
        [MaxLength(200,ErrorMessage = "Your description must be shorter than 200 characters")]
        public string Description { get; set; }
    }
}
