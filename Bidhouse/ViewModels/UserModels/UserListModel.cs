using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class UserListModel
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Rating { get; set; }
        public int? NumberOfPosts { get; set; }


    }
}
