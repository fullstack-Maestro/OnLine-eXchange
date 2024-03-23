using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.FavouritePosts;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;
using System.ComponentModel.Design;

namespace Olx.Service.Services;

public class FavourtePostService : IFavouritePostService
{
    private readonly IRepository<FavouritePost> favouritePostRepository;
    public FavourtePostService(IRepository<FavouritePost> favouritePostRepository)
    {
        this.favouritePostRepository = favouritePostRepository;
    }
    public async Task<FavouritePostViewDto> CreateAsync(FavouritePostCreateDto favouritePost)
    {
        var existFavouritePost = await favouritePostRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(f => f.UserId == favouritePost.UserId && f.PostId == favouritePost.PostId);
        if (existFavouritePost != null && existFavouritePost.IsDeleted)
            return await UpdateAsync(existFavouritePost.Id, favouritePost.MapTo<FavouritePostUpdateDto>(), true);

        if (existFavouritePost != null)
            throw new Exception("Already exist");

        var createUser = await favouritePostRepository.InsertAsync(existFavouritePost);
        await favouritePostRepository.SaveChangesAsync();

        return createUser.MapTo<FavouritePostViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existFavouritePost = await favouritePostRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        existFavouritePost.IsDeleted = true;
        existFavouritePost.DeletedAt = DateTime.UtcNow;

        await favouritePostRepository.DeleteAsync(existFavouritePost);
        await favouritePostRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<FavouritePostViewDto>> GetAllAsync()
    {
        return await Task.FromResult(favouritePostRepository.SelectAllAsQueryable().MapTo<FavouritePostViewDto>());
    }

    public async Task<FavouritePostViewDto> GetByIdAsync(long id)
    {

        var existFavouritePost = await favouritePostRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        return existFavouritePost.MapTo<FavouritePostViewDto>();
    }

    public async Task<FavouritePostViewDto> UpdateAsync(long id, FavouritePostUpdateDto favouritePost, bool isDeleted = false)
    {
        var existFavouritePost = new FavouritePost();

        if (isDeleted)
        {
            existFavouritePost = await favouritePostRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(u => u.Id == id);
            existFavouritePost.IsDeleted = false;
        }

        existFavouritePost.UserId = favouritePost.UserId;
        existFavouritePost.PostId = favouritePost.PostId;
        existFavouritePost.UpdatedAt = DateTime.UtcNow;

        await favouritePostRepository.UpdateAsync(existFavouritePost);
        await favouritePostRepository.SaveChangesAsync();

        return existFavouritePost.MapTo<FavouritePostViewDto>();
    }
}
