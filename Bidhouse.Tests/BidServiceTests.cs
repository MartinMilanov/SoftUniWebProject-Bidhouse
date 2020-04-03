using Bidhouse.Models;
using Bidhouse.Services.Bids;
using Bidhouse.Services.Posts;
using Bidhouse.Tests.Factories;
using Bidhouse.ViewModels.BidModels;
using Bidhouse.ViewModels.PostModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bidhouse.Tests
{
    public class BidServiceTests
    {
        public ApplicationDbContext context { get; set; }
        public IPostService postService { get; set; }
        public IBidService bidService { get; set; }
        public ServiceFactory services { get; set; }
        public CreatePostInputModel postDummy { get; set; }
        public User bidSenderDummy { get; set; }
        public User bidReceiverDummy { get; set; }
        public BidServiceTests()
        {

            this.services = new ServiceFactory();
            this.context = services.Context;
            this.postService = new PostService(context);
            this.bidService = new BidService(context);

            this.postDummy = new CreatePostInputModel()
            {
                Name = "Test"
            };

            this.bidReceiverDummy = new User()
            {
                UserName = "Receiver"
            };

            this.bidSenderDummy = new User()
            {
                UserName = "Sender"
            };
        }

        [Fact]
        public async Task PlaceBidShouldCreateBid()
        {
            await this.services.UserManager
                .CreateAsync(this.bidReceiverDummy,"12345678");
            
            await this.services.UserManager
                .CreateAsync(this.bidSenderDummy, "12345678");
           
            var postId = await this.postService
                .CreatePost(postDummy, bidReceiverDummy.Id);

            var input = new BidInputModel()
            {
                Description = "TestDescription",
                Price = 1000,
                Days = 14,
                PostId = postId,
                ReceiverId = this.bidReceiverDummy.Id
            };


            await this.bidService.PlaceBid(input, this.bidSenderDummy.Id);

            var result = await this.context
                .Posts
                .Include(x => x.Bids)
                .FirstOrDefaultAsync(x => x.Id == postId);

            Assert.True(result.Bids.Count > 0);
            
        }
   
        [Fact]
        public async Task GetBidsShouldGetBids()
        {
            await this.services.UserManager
                          .CreateAsync(this.bidReceiverDummy, "12345678");

            await this.services.UserManager
                .CreateAsync(this.bidSenderDummy, "12345678");

            var postId = await this.postService
                .CreatePost(postDummy, bidReceiverDummy.Id);

            var input = new BidInputModel()
            {
                Description = "TestDescription",
                Price = 1000,
                Days = 14,
                PostId = postId,
                ReceiverId = this.bidReceiverDummy.Id
            };


            await this.bidService.PlaceBid(input, this.bidSenderDummy.Id);

            var result = await this.bidService.GetBids(this.bidSenderDummy.Id);

            Assert.NotNull(result);
            Assert.True(result.BidsSent.Count > 0);
        }
    
        [Fact]
        public async Task ApproveBidShouldChangeStatusOfBid()
        {
            await this.services.UserManager
              .CreateAsync(this.bidReceiverDummy, "12345678");

            await this.services.UserManager
                .CreateAsync(this.bidSenderDummy, "12345678");

            var postId = await this.postService
                .CreatePost(postDummy, bidReceiverDummy.Id);

            var input = new BidInputModel()
            {
                Description = "TestDescription",
                Price = 1000,
                Days = 14,
                PostId = postId,
                ReceiverId = this.bidReceiverDummy.Id
            };


            await this.bidService.PlaceBid(input, this.bidSenderDummy.Id);

            var bid = await this.context.Bids.FirstAsync();

            var result = await this.bidService.ApproveBid(bid.Id, postId);

            Assert.True(result);
            Assert.True(bid.StatusOfBid == Status.Approved);
        }
    
        [Fact]
        public async Task HasBidShouldReturnTrue()
        {
            await this.services.UserManager
              .CreateAsync(this.bidReceiverDummy, "12345678");

            await this.services.UserManager
                .CreateAsync(this.bidSenderDummy, "12345678");

            var postId = await this.postService
                .CreatePost(postDummy, bidReceiverDummy.Id);

            var input = new BidInputModel()
            {
                Description = "TestDescription",
                Price = 1000,
                Days = 14,
                PostId = postId,
                ReceiverId = this.bidReceiverDummy.Id
            };


            await this.bidService.PlaceBid(input, this.bidSenderDummy.Id);

            var result = await this.bidService.HasBid(postId, this.bidSenderDummy.Id);

            Assert.True(result);

        }
    
        [Fact]
        public async Task HasBidShouldReturnFalse()
        {
            await this.services.UserManager
              .CreateAsync(this.bidReceiverDummy, "12345678");

            await this.services.UserManager
                .CreateAsync(this.bidSenderDummy, "12345678");

            var postId = await this.postService
                .CreatePost(postDummy, bidReceiverDummy.Id);

            var input = new BidInputModel()
            {
                Description = "TestDescription",
                Price = 1000,
                Days = 14,
                PostId = postId,
                ReceiverId = this.bidReceiverDummy.Id
            };


            await this.bidService.PlaceBid(input, this.bidSenderDummy.Id);

            var result = await this.bidService.HasBid(postId, bidReceiverDummy.Id);

            Assert.False(result);
        }
    }
}
