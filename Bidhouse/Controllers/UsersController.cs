using Bidhouse.Services;
using Bidhouse.Services.Files;
using Bidhouse.Services.Users;
using Bidhouse.ViewModels.AuthModels;
using Bidhouse.ViewModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IFileService fileService;

        public UsersController(IUserService userService,IAuthService authService,IFileService fileService)
        {
            this.userService = userService;
            this.authService = authService;
            this.fileService = fileService;
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

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers(int startAt, int count)
        {
            var users = await this.userService.GetUsers(startAt, count);
            if (users == null)
            {
                return Ok(users);
            }

            return Ok(users);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser(string id,[FromForm]UserUpdateModel input)
        {
            if (id != this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            var files = Request.Form.Files;
            var filePath = await this.fileService.UploadFile(files);

            var result = await this.userService.UpdateUser(id,input,filePath);
            if (result == false)
            {
                return BadRequest("Could not update user!");
            }

            return Ok();


        }
    }
}
