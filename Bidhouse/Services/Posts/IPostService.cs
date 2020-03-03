﻿using Bidhouse.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Posts
{
    public interface IPostService
    {
        public Task<string> CreatePost(CreatePostInputModel input, string id);
    }
}
