using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.BidModels
{
    public class BidApproveInputModel
    {
        [Required(ErrorMessage = "You should provide the bid's id")]
        public string BidId { get; set; }
        [Required(ErrorMessage = "You should provide the post's id")]
        public string PostId { get; set; }
    }
}
