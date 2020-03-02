using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.UserModels
{
    public class GetUsersInputModel
    {
        public int StartAt { get; set; }
        public int Count { get; set; }
        public string SearchInput { get; set; }

    }
}
