using Bidhouse.Models;
using Bidhouse.ViewModels.BidModels;
using Bidhouse.ViewModels.PostModels;
using Bidhouse.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public async Task<PostDetailViewModel> GetPost(string id)
        {
            var query = await this.db.Posts
                .Include(x => x.Creator)
                .ThenInclude(x => x.ReviewsGotten)
                .Include(b => b.Bids)
                .ThenInclude(x => x.Bidder)
                .ThenInclude(x => x.ReviewsGotten)
                .FirstOrDefaultAsync(x => x.Id == id);


            var post = new PostDetailViewModel()
            {
                Name = query.Name,
                Description = query.Description,
                Location = query.Location,
                TimeDue = query.TimeDue,
                Price = query.Price,
                CreatedOn = query.CreatedOn,
                Status = query.Status.ToString(),
                Creator = query.Creator != null ? new UserListModel
                {
                    Id = query.Creator.Id,
                    Name = query.Creator.UserName,
                    ImageUrl = query.Creator.ImageUrl,
                    Rating = query.Creator.ReviewsGotten.Count > 0 ? query.Creator.ReviewsGotten.Sum(x => x.Rating) : 0

                }:null,
                Bids = query.Bids.Count > 0 || query.Bids != null ? query.Bids.Select(x => new BidViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,
                    Days = x.Days,
                    Status = x.StatusOfBid.ToString(),
                    CreatedOn = x.CreatedOn,
                    Bidder = new UserListModel
                    {
                        Id = x.Bidder.Id,
                        Name = x.Bidder.UserName,
                        ImageUrl = x.Bidder.ImageUrl,
                        Rating = x.Bidder.ReviewsGotten.Count > 0 ? query.Creator.ReviewsGotten.Sum(x => x.Rating) : 0
                    }
                }).ToList() : null
            };

            return post;
        }

        public async Task<string> CreatePost(CreatePostInputModel input, string id)
        {

            var post = new Post()
            {
                Name = input.Name,
                Description = input.Description,
                Location = input.Location != null ? input.Location : "",
                TimeDue = input.Time,
                Category = input.Category,
                Price = input.Price,
                Creator = this.db.Users.FirstOrDefault(x => x.Id == id),
                CreatedOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            };
            await this.db.Posts.AddAsync(post);
            await this.db.SaveChangesAsync();

            return post.Id;

        }

        public async Task<bool> DeletePost(string id)
        {
            var post = this.db.Posts.FirstOrDefault(x => x.Id == id);
            var reports = await this.db.Reports
                .Include(x => x.ReportedPost)
                .Where(x => x.ReportedPostId == post.Id)
                .ToListAsync();
            foreach (var report in reports)
            {
                this.db.Reports.Remove(report);
            }

            this.db.Posts.Remove(post);
            await this.db.SaveChangesAsync();

            return true;

        }

        public async Task<bool> IsClosed(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new Exception("Id is null");
            }
            var result = (await this.db.Posts.FirstOrDefaultAsync(x => x.Id == id)).Status == Status.Closed ? true : false;

            return result;
        }

        public async Task<ICollection<PostListViewModel>> GetPosts(GetPostsInputModel input)
        {
            
            if (String.IsNullOrEmpty(input.Category) && String.IsNullOrEmpty(input.SearchInput))
            {
                var result = await this.db.Posts
                 .Include(x => x.Creator)
                 .Skip(input.SkipCount)
                 .Take(input.TakeCount)
                 .Select(x => new PostListViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     Category = x.Category.ToString(),
                     Location = x.Location,
                     CreatedOn = x.CreatedOn,
                     Price = x.Price,
                     UserId = x.CreatorId,
                     UserName = x.Creator.UserName,
                     UserImageUrl = x.Creator.ImageUrl
                 }
                ).ToListAsync();

                return result;

            }
            
            else if(String.IsNullOrEmpty(input.Category)==false && String.IsNullOrEmpty(input.SearchInput))
            {

            var result = await this.db.Posts
                 .Include(x => x.Creator)
                 .Where(x=>x.Category == (Category)(int.Parse(input.Category)))
                 .Skip(input.SkipCount)
                 .Take(input.TakeCount)
                 .Select(x => new PostListViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     Category = x.Category.ToString(),
                     Location = x.Location,
                     CreatedOn = x.CreatedOn,
                     Price = x.Price,
                     UserId = x.CreatorId,
                     UserName = x.Creator.UserName,
                     UserImageUrl = x.Creator.ImageUrl
                 }
                ).ToListAsync();

            return result;
            }

            else if (String.IsNullOrEmpty(input.Category) && String.IsNullOrEmpty(input.SearchInput) == false)
            {

                var result = await this.db.Posts
                     .Include(x => x.Creator)
                     .Where(x => x.Name.ToLower().Contains(input.SearchInput.ToLower()))
                     .Skip(input.SkipCount)
                     .Take(input.TakeCount)
                     .Select(x => new PostListViewModel
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         Category = x.Category.ToString(),
                         Location = x.Location,
                         CreatedOn = x.CreatedOn,
                         Price = x.Price,
                         UserId = x.CreatorId,
                         UserName = x.Creator.UserName,
                         UserImageUrl = x.Creator.ImageUrl
                     }
                    ).ToListAsync();

                return result;
            }

            else
            {

                var result = await this.db.Posts
                     .Include(x => x.Creator)
                     .Where(x => x.Name.ToLower().Contains(input.SearchInput.ToLower()))
                     .Where(x=>x.Category == (Category)(int.Parse(input.Category)))
                     .Skip(input.SkipCount)
                     .Take(input.TakeCount)
                     .Select(x => new PostListViewModel
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         Category = x.Category.ToString(),
                         Location = x.Location,
                         CreatedOn = x.CreatedOn,
                         Price = x.Price,
                         UserId = x.CreatorId,
                         UserName = x.Creator.UserName,
                         UserImageUrl = x.Creator.ImageUrl
                     }
                    ).ToListAsync();

                return result;
            }

        }

        public async Task<ICollection<PostListViewModel>> GetPostsById(string id)
        {
            var posts = await this.db.Posts
                 .Include(x => x.Creator)
                 .Where(x => x.CreatorId == id)
                 .Select(x => new PostListViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     CreatedOn = x.CreatedOn,
                     Price = x.Price
                 }
                ).ToListAsync();

            return posts;
        }

        public async Task<string> UpdatePost(PostUpdateInputModel input,string currentUserId)
        {
            var result = await this.db.Posts.AnyAsync(x => x.Id == input.PostId && x.CreatorId == currentUserId);

            if (result == false)
            {
                return "Could not find post...";
            }
            var post = await this.db.Posts.FindAsync(input.PostId);

            post.Description = input.Description;
            post.Price = input.Price;
            post.TimeDue = input.Date;
            post.Location = input.Location;

            this.db.Posts.Update(post);
            await this.db.SaveChangesAsync();

            return "Post updated successfully";
        }
    }
}
