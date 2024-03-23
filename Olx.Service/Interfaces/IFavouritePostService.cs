using Olx.Service.DTOs.FavouritePosts;

namespace Olx.Service.Interfaces;

public interface IFavouritePostService
{
    Task<FavouritePostViewDto> CreateAsync(FavouritePostCreateDto favourite);
    Task<FavouritePostViewDto> UpdateAsync(long id, FavouritePostUpdateDto favourite, bool isDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<FavouritePostViewDto> GetByIdAsync(long id);
    Task<IEnumerable<FavouritePostViewDto>> GetAllAsync();
}
