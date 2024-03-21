using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class FavouritePostRepository : IFavouritePostRepository
{
    private readonly AppDbContext _dbContext;

    public FavouritePostRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FavouritePost> GetFavouritePostById(long favouritePostId)
    {
        return await _dbContext.FavouritePost.FindAsync(favouritePostId);
    }

    public async Task<List<FavouritePost>> GetAllFavouritePosts()
    {
        return await _dbContext.FavouritePost.ToListAsync();
    }

    public void AddFavouritePost(FavouritePost favouritePost)
    {
        _dbContext.FavouritePost.Add(favouritePost);
    }

    public void UpdateFavouritePost(FavouritePost favouritePost)
    {
        _dbContext.FavouritePost.Update(favouritePost);
    }

    public void DeleteFavouritePost(FavouritePost favouritePost)
    {
        _dbContext.FavouritePost.Remove(favouritePost);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
