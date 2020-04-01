using Bidhouse.Models;
using Bidhouse.Services.Posts;
using Bidhouse.Tests.Factories;
using Bidhouse.ViewModels.PostModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Bidhouse.Tests
{
    public class PostServiceTests
    {
        public ApplicationDbContext context { get; set; }
        public IPostService postService { get; set; }
        public PostServiceTests()
        {
            var services = new ServiceFactory();
            this.context = services.Context;
            this.postService = new PostService(context);
        }
        [Fact]
        public async Task ShouldCreatePost()
        {
            var post = new Post()
            {
                Name = "TestPost"
            };
            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            var result = await this.context.Posts.AnyAsync(x => x.Id == post.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldGetPost()
        {
            var post = new Post()
            {
                Name = "TestPost"
            };
            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            var result = await this.postService.GetPost(post.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdatePost()
        {
            var post = new Post()
            {
                Name = "TestPost",
                Description = "This is my description",
                CreatorId = "1"
            };

            var oldDescription = post.Description;

            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            var input = new PostUpdateInputModel()
            {
                PostId = post.Id,
                Description = "New Description"
            };

            await this.postService.UpdatePost(input,"1");

            var isUpdated = post.Description != oldDescription ? true : false;
            

            Assert.True(isUpdated);
        }
        
        [Fact]
        public async Task ShouldDeletePost()
        {
            var post = new Post()
            {
                Name = "TestPost"
            };
            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            var result = await this.postService.DeletePost(post.Id);

            Assert.True(result);
        }

    }
}
