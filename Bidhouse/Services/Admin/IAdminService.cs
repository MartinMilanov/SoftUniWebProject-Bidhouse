using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Admin
{
    public interface IAdminService
    {
        public Task<string> DeleteUser(string id);

        public Task<string> MakeAdmin(string id);
    }
}
