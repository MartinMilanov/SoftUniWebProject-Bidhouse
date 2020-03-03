using Bidhouse.Models;
using Bidhouse.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Posts
{
    public class PostService:IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<string> CreatePost(CreatePostInputModel input, string id)
        {

            var post = new Post()
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                Creator = this.db.Users.FirstOrDefault(x => x.Id == id)
            };
            await this.db.Posts.AddAsync(post);
            await this.db.SaveChangesAsync();

            return post.Id;

        }
    }
}
