using Bidhouse.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Users
{
    public interface IUserService
    {
        public Task<bool> UserExists(string id);
        public Task<ICollection<UserListModel>> GetUsers(GetUsersInputModel input);
        public Task<UserDetailsModel> GetUser(string id);
        public Task<string> GetUserImageUrl(string id);
        public Task<bool> UpdateUser(string id, UserUpdateModel input,string imageUrl);
    }
}
