using Olx.Service.DTOs.Posts;

namespace Olx.Service.Interfaces;

public interface IPostService
{
    Task<PostViewDto> CreateAsync(PostCreateDto post);
    Task<PostViewDto> UpdateAsync(long id, PostUpdateDto post);
    Task<bool> DeleteAsync(long id);
    Task<PostViewDto> GetByIdAsync(long id);
    Task<IEnumerable<PostViewDto>> GetAllAsync();
}
