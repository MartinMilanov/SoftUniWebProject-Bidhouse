using Bidhouse.Models;
using Bidhouse.Services.Admin;
using Bidhouse.Services.Posts;
using Bidhouse.Tests.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bidhouse.Tests
{
    public class AdminServiceTests
    {
        public IAdminService adminService { get; set; }

        public ServiceFactory services { get; set; }

        public User TestUser { get; set; }

        public AdminServiceTests()
        {
            this.services = new ServiceFactory();
            this.adminService = new AdminService(services.UserManager, services.Context, new PostService(services.Context));

            this.TestUser = new User()
            {
                UserName = "Test"
            };

        }

        [Fact]
        public async Task DeleteUserShouldDeleteUser()
        {

            await this.services.UserManager.CreateAsync(this.TestUser, "12345678");

            await this.adminService.DeleteUser(this.TestUser.Id);
            var result = await this.services.UserManager.FindByNameAsync(this.TestUser.UserName);

            Assert.Null(result);
        }
    
        [Fact]
        public async Task MakeAdminShouldMakeAdmin()
        {
            await this.services.RoleManager.CreateAsync(new Role()
            {
                Name = "User",
                NormalizedName = "User"
            });

            await this.services.RoleManager.CreateAsync(new Role()
            {
                Name = "Admin",
                NormalizedName = "Admin"
            });

            await this.services.UserManager.CreateAsync(this.TestUser, "12345678");

            var user = await this.services.UserManager.FindByNameAsync(this.TestUser.UserName);

            await this.services.UserManager.AddToRoleAsync(user, "User");

            await this.adminService.MakeAdmin(user.Id);

            user = await this.services.UserManager.FindByNameAsync(user.UserName);

            var role = await this.services.UserManager.GetRolesAsync(user);

            Assert.True(role.Contains("Admin"));
        }
    
    }
}
