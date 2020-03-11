using Bidhouse.Models;
using Bidhouse.Services;
using Bidhouse.ViewModels.AuthModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bidhouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;

        public AuthController(IAuthService authService,IConfiguration configuration)
        {
            this.authService = authService;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterInputModel input)
        {
            input.Username = input.Username.ToLower();
            if (await authService.UserExists(input.Username) == true)
            {
                return BadRequest("Username already exists");
            }
            var user = new User() {
                Username = input.Username,
                WorkPosition = input.JobPosition,
                City = input.City,
                ImageUrl = "/images/default-user-photo.png"
            };
            var createdUser = await this.authService.Register(user, input.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginInputModel input)
        {

            User userReturned = await this.authService.Login(input.Username.ToLower(), input.Password);

            if (userReturned == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userReturned.Id.ToString()),
                new Claim(ClaimTypes.Name,userReturned.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> ChangePassword(string id, [FromBody]ChangePasswordInputModel input)
        {
            if (id != this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            if (input.NewPassword != input.ConfirmPassword)
            {
                return BadRequest("Passwords must match");
            }
          

            var result = await this.authService.ChangePassword(id, input);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}
