using Bidhouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services
{
    public interface IAuthService
    {
        public Task<User> Login(string username, string password);
        public Task<User> Register(User user, string password);
        public Task<bool> UserExists(string username);
    }
}
