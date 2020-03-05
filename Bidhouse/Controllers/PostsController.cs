using Bidhouse.Services.Posts;
using Bidhouse.ViewModels.PostModels;
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
    public class PostsController:ControllerBase
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody]CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest("Wrong values");
            }


            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await this.postService.CreatePost(input, id);

            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetPost(string id)
        {
            var post = await this.postService.GetPost(id);

            return Ok(post);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(string id)
        {
            await this.postService.DeletePost(id);

            return Ok();
        }

    }
}
