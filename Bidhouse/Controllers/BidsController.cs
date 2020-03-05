﻿using Bidhouse.Services.Bids;
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
        public async Task<IActionResult> PlaceBid([FromBody]BidInputModel input)
        {


            if (!this.ModelState.IsValid)
            {
                return BadRequest("Values are not compatible");
            }
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
                return BadRequest("Something went terribly wrong !");
            }
        }
    }
}