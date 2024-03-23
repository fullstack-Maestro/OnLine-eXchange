using Olx.Service.DTOs.FavouritePosts;

namespace Olx.Service.Interfaces;

public interface IFavouritePostService
{
    /// <summary>
    /// Create new favouritePost
    /// </summary>
    /// <param name="favourite"></param>
    /// <returns></returns>
    Task<FavouritePostViewDto> CreateAsync(FavouritePostCreateDto favourite);

    /// <summary>
    /// Update exist favouritePost
    /// </summary>
    /// <param name="id"></param>
    /// <param name="favourite"></param>
    /// <returns></returns>
    Task<FavouritePostViewDto> UpdateAsync(long id, FavouritePostUpdateDto favourite, bool isUsesDeleted);

    /// <summary>
    /// Delete exist favouritePost via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist favouritePost via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<FavouritePostViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist favouritePosts
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FavouritePostViewDto>> GetAllAsync();
}
