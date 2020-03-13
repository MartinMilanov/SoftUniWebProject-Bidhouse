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
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreatePost([FromBody]CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest("Wrong values");
            }


            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await this.postService.CreatePost(input, id);

            return this.StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult<PostDetailViewModel>> GetPost(string id)
        {
            var post = await this.postService.GetPost(id);

            return Ok(post);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePost(string id)
        {
            await this.postService.DeletePost(id);

            return Ok();
        }

        [HttpGet("getPosts")]
        public async Task<ActionResult<ICollection<PostListViewModel>>> GetPosts()
        {
            var posts = await this.postService.GetPosts();

            return Ok(posts);

        }
        [HttpGet("getPostsById")]
        public async Task<ActionResult<ICollection<PostListViewModel>>> GetPostsById(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("You should specify an id !");
            }
            var posts = await this.postService.GetPostsById(id);


            return Ok(posts);

        }
    }
}
