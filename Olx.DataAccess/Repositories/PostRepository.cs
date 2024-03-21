using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _dbContext;

    public PostRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post> GetPostById(long postId)
    {
        return await _dbContext.Post.FindAsync(postId);
    }

    public async Task<List<Post>> GetAllPosts()
    {
        return await _dbContext.Post.ToListAsync();
    }

    public void AddPost(Post post)
    {
        _dbContext.Post.Add(post);
    }

    public void UpdatePost(Post post)
    {
        _dbContext.Post.Update(post);
    }

    public void DeletePost(Post post)
    {
        _dbContext.Post.Remove(post);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}