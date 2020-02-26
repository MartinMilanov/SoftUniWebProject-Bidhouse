using Bidhouse.Services;
using Bidhouse.Services.Users;
using Bidhouse.ViewModels.AuthModels;
using Bidhouse.ViewModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bidhouse.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;

        public UsersController(IUserService userService,IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await this.userService.GetUser(id);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody]UserUpdateModel input)
        {
            if (id != this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            var result = await this.userService.UpdateUser(id, input);
            if (result == false)
            {
                return BadRequest("Could not update user!");
            }

            return Ok();
        }
    }
}
