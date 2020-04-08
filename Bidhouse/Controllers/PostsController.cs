using Bidhouse.Services.Posts;
using Bidhouse.ViewModels.PostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            //if (!this.ModelState.IsValid)
            //{
            //    return BadRequest("Wrong values");
            //}


            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await this.postService.CreatePost(input, id);

            if (String.IsNullOrEmpty(result))
            {
                return BadRequest("Could not create post");
            }

            return this.StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult<PostDetailViewModel>> GetPost(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Please provide the post's id");
            }
            var post = await this.postService.GetPost(id);

            return Ok(post);
        }


        public async Task<ActionResult<string>> UpdatePost([FromBody]PostUpdateInputModel input)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await this.postService.UpdatePost(input,currentUserId);

            if (result == "Could not find post...")
            {
                return NotFound(new { result });
            }

            return Ok(new { result });
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePost(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Please provide the post's id");
            }
            await this.postService.DeletePost(id);

            return Ok();
        }

        [HttpGet("getPosts")]
        public async Task<ActionResult<ICollection<PostListViewModel>>> GetPosts(GetPostsInputModel input)
        {
            var posts = await this.postService.GetPosts(input);

            return Ok(posts);

        }
        [HttpGet("getPostsById")]
        public async Task<ActionResult<ICollection<PostListViewModel>>> GetPostsById(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Please provide the post's id");
            }
            var posts = await this.postService.GetPostsById(id);


            return Ok(posts);

        }
    }
}
