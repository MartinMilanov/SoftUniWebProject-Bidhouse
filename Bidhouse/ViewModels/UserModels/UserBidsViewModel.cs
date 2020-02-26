using Bidhouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class UserBidsViewModel
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string PostName { get; set; }
        public Status Status { get; set; }
        public decimal Price { get; set; }

    }
}
