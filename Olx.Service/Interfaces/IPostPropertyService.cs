using Olx.Service.DTOs.PostProperties;

namespace Olx.Service.Interfaces;

public interface IPostPropertyService
{
    Task<PostPropertyViewDto> CreateAsync(PostPropertyCreateDto post);
    Task<PostPropertyViewDto> UpdateAsync(long id, PostPropertyUpdateDto post, bool isDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<PostPropertyViewDto> GetByIdAsync(long id);
    Task<IEnumerable<PostPropertyViewDto>> GetAllAsync();
}
