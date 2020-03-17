using Bidhouse.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController:ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            var result = await this.adminService.DeleteUser(id);
            if (result == "User not found" )
            {
                return BadRequest(result);
            }
            if (result == "Could not delete user")
            {
                return BadRequest(result);
            }

            return Ok(new { result });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> MakeAdmin(string id)
        {
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
