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

    public async Task<PostProperty> GetPostPropertyById(long postPropertyId)
    {
        return await _dbContext.PostProperty.FindAsync(postPropertyId);
    }

    public async Task<List<PostProperty>> GetAllPostProperties()
    {
        return await _dbContext.PostProperty.ToListAsync();
    }

    public void AddPostProperty(PostProperty postProperty)
    {
        _dbContext.PostProperty.Add(postProperty);
    }

    public void UpdatePostProperty(PostProperty postProperty)
    {
        _dbContext.PostProperty.Update(postProperty);
    }

    public void DeletePostProperty(PostProperty postProperty)
    {
        _dbContext.PostProperty.Remove(postProperty);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}