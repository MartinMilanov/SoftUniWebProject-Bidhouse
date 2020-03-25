using Bidhouse.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Posts
{
    public interface IPostService
    {
        public Task<PostDetailViewModel> GetPost(string id);
        public Task<string> CreatePost(CreatePostInputModel input, string id);
        public Task<string> UpdatePost(PostUpdateInputModel input,string currentUserId);
        public Task<bool> DeletePost(string id);
        public Task<ICollection<PostListViewModel>> GetPosts(GetPostsInputModel input);
        public Task<ICollection<PostListViewModel>> GetPostsById(string id);
        public Task<bool> IsClosed(string id);

    }
}
