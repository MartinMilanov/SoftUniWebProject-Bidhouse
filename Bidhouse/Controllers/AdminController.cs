using Bidhouse.Services.Admin;
using Bidhouse.Services.Files;
using Bidhouse.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController:ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IFileService fileService;
        private readonly IUserService userService;

        public AdminController(IAdminService adminService, IFileService fileService, IUserService userService)
        {
            this.adminService = adminService;
            this.fileService = fileService;
            this.userService = userService;
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Please provide the user's id");
            }
            var userImageUrl = await this.userService.GetUserImageUrl(id);
            var result = await this.adminService.DeleteUser(id);
            if (result == "User not found" )
            {
                return BadRequest(result);
            }
            if (result == "Could not delete user")
            {
                return BadRequest(result);
            }
            this.fileService.RemoveImage(userImageUrl);

            return Ok(new { result });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> MakeAdmin(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Please provide the user's id");
            }
            var result = await  this.adminService.MakeAdmin(id);
            if (result == "User not found")
            {
                return BadRequest(result);
            }
            if (result == "User is already admin")
            {
                return BadRequest(result);
            }
            return Ok(new { result });
        }
    }
}
