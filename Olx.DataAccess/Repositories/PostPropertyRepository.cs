using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class PostPropertyRepository : IPostPropertyRepository
{
    private readonly AppDbContext _dbContext;

    public PostPropertyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PostProperty> GetPostPropertyById(int postPropertyId)
    {
        return await _dbContext.PostProperties.FindAsync(postPropertyId);
    }

    public async Task<List<PostProperty>> GetAllPostProperties()
    {
        return await _dbContext.PostProperties.ToListAsync();
    }

    public void AddPostProperty(PostProperty postProperty)
    {
        _dbContext.PostProperties.Add(postProperty);
    }

    public void UpdatePostProperty(PostProperty postProperty)
    {
        _dbContext.PostProperties.Update(postProperty);
    }

    public void DeletePostProperty(PostProperty postProperty)
    {
        _dbContext.PostProperties.Remove(postProperty);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}