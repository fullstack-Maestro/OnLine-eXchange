using Olx.Service.DTOs.PostProperties;

namespace Olx.Service.Interfaces;

public interface IPostPropertyService
{
    /// <summary>
    /// Create new postProperty
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    Task<PostPropertyViewDto> CreateAsync(PostPropertyCreateDto post);

    /// <summary>
    /// Update exist postProperty
    /// </summary>
    /// <param name="id"></param>
    /// <param name="post"></param>
    /// <returns></returns>
    Task<PostPropertyViewDto> UpdateAsync(long id, PostPropertyUpdateDto post, bool isDeleted = false);

    /// <summary>
    /// Delete exist postProperty via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist postProperty via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PostPropertyViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist postProperties 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PostPropertyViewDto>> GetAllAsync();
}
