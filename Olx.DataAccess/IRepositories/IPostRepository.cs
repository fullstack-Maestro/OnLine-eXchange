using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        void DeletePost(Post post);
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostById(int postId);
        Task SaveChangesAsync();
        void UpdatePost(Post post);
    }
}