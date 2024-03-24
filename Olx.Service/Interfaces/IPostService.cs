using Olx.Service.DTOs.Posts;

namespace Olx.Service.Interfaces;

public interface IPostService
{
    /// <summary>
    /// Create new post
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    Task<PostViewDto> CreateAsync(PostCreateDto post);

    /// <summary>
    /// Update exist post
    /// </summary>
    /// <param name="id"></param>
    /// <param name="post"></param>
    /// <param name="isDeleted"></param>
    /// <returns></returns>
    Task<PostViewDto> UpdateAsync(long id, PostUpdateDto post, bool isDeleted = false);

    /// <summary>
    /// Delete exist post via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist post via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PostViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist posts
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PostViewDto>> GetAllAsync();
}
