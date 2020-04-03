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
        public ServiceFactory services { get; set; }
        public IPostService postService { get; set; }

        public User userDummy { get;set; }
        public CreatePostInputModel createPostModelDummy { get; set; }
        public PostServiceTests()
        {
            this.services = new ServiceFactory();
            this.postService = new PostService(this.services.Context);

            this.userDummy = new User()
            {
                UserName = "Test"
            };



            this.createPostModelDummy = new CreatePostInputModel()
            {
                Name = "Test",
                Description = "Test Description",
                Price = 2300
            };
        }
        [Fact]
        public async Task ShouldCreatePost()
        {
            this.services.UserManager.CreateAsync(userDummy, "123456778").Wait();

            var result = await this.postService.CreatePost(createPostModelDummy, userDummy.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetPost()
        {
            this.services.UserManager.CreateAsync(userDummy, "123456778").Wait();

            var postId = await this.postService.CreatePost(createPostModelDummy, userDummy.Id);

            var result = await this.postService.GetPost(postId);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdatePost()
        {
            this.services.UserManager.CreateAsync(userDummy, "123456778").Wait();

            var postId = await this.postService.CreatePost(createPostModelDummy, userDummy.Id);

            var input = new PostUpdateInputModel()
            {
                PostId = postId,
                Description = "New Description"
            };

            await this.postService.UpdatePost(input,"1");

            var isUpdated = createPostModelDummy.Description != input.Description ? true : false;
            

            Assert.True(isUpdated);
        }
        
        [Fact]
        public async Task ShouldDeletePost()
        {
            this.services.UserManager.CreateAsync(userDummy, "123456778").Wait();

            var postId = await this.postService.CreatePost(createPostModelDummy, userDummy.Id);

            var result = await this.postService.DeletePost(postId);
            var secondResult = await this.services.Context.Posts.AnyAsync(x => x.Id == postId);

            Assert.True(result);
            Assert.False(secondResult);
        }

    }
}
