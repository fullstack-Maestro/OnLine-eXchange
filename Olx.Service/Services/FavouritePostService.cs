using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Categories;
using Olx.Service.DTOs.FavouritePosts;
using Olx.Service.Exceptions;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

namespace Olx.Service.Services;

public class FavouritePostService : IFavouritePostService
{
    private readonly IRepository<FavouritePost> favouritePostRepository;
    public FavouritePostService(IRepository<FavouritePost> favouritePostRepository)
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
            throw new CustomException(409, "FavouritePost already exist");
        existFavouritePost = favouritePost.MapTo<FavouritePost>();
        var createFavouritePost = await favouritePostRepository.InsertAsync(existFavouritePost);
        await favouritePostRepository.SaveAsync();

        return createFavouritePost.MapTo<FavouritePostViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existFavouritePost = await favouritePostRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "FavouritePost not found");

        existFavouritePost.IsDeleted = true;
        existFavouritePost.DeletedAt = DateTime.UtcNow;

        await favouritePostRepository.DeleteAsync(existFavouritePost);
        await favouritePostRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<FavouritePostViewDto>> GetAllAsync()
    {
        return await Task.FromResult(favouritePostRepository.SelectAllAsQueryable()
                .Where(c => !c.IsDeleted)
                .MapTo<FavouritePostViewDto>());
    }

    public async Task<FavouritePostViewDto> GetByIdAsync(long id)
    {
        var existFavouritePost = await favouritePostRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "FavouritePost not found");

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
        await favouritePostRepository.SaveAsync();

        return existFavouritePost.MapTo<FavouritePostViewDto>();
    }
}
