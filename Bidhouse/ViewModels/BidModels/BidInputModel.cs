using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.BidModels
{
    public class BidInputModel
    {
        [Required]
        [StringLength(270,ErrorMessage = "Description must be between 10 and 270 characters",MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        public int Days { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string PostId   { get; set; }
        [Required]
        public string ReceiverId { get; set; }
    }
}
