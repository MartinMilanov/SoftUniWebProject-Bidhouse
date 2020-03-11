using Bidhouse.Services.Bids;
using Bidhouse.Services.Posts;
using Bidhouse.ViewModels.BidModels;
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
    public class BidsController:ControllerBase
    {
        private readonly IBidService bidService;
        private readonly IPostService postService;

        public BidsController(IBidService bidService,IPostService postService)
        {
            this.bidService = bidService;
            this.postService = postService;
        }
        [HttpPost]
        public async Task<ActionResult> PlaceBid([FromBody]BidInputModel input)
        {

            var receiverId = input.ReceiverId;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (receiverId == userId)
            {
                return BadRequest("Can't send bid to your own post");
            }

            var hasBid = await this.bidService.HasBid(input.PostId, userId);

            if (hasBid == true)
            {
                return BadRequest("You can't bid twice");
            }

            var result = await this.bidService.PlaceBid(input, userId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong ");
            }
        }

        [HttpPost("approveBid")]
        public async Task<ActionResult> ApproveBid([FromBody]BidApproveInputModel input)
        {
            if (String.IsNullOrEmpty(input.BidId) || String.IsNullOrEmpty(input.PostId))
            {
                return BadRequest("The id of the bid cannot be null or empty.");
            }
            if (await postService.IsClosed(input.PostId))
            {
                return BadRequest("The post is closed and cannot be bid on");
            }
            var result = await this.bidService.ApproveBid(input.BidId,input.PostId);

            if (result == false)
            {
                return BadRequest("Something went wrong");
            }
            
            return Ok();
        }
    }
}
