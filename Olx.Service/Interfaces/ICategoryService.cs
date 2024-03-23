using Olx.Service.DTOs.Categories;

namespace Olx.Service.Interfaces;

public interface ICategoryService
{
    /// <summary>
    /// Create new category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    Task<CategoryViewDto> CreateAsync(CategoryCreateDto category);

    /// <summary>
    /// Update exist category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="category"></param>
    /// <returns></returns>

    Task<CategoryViewDto> UpdateAsync(long id, CategoryUpdateDto category,bool isDeleted = false);

    /// <summary>
    /// Delete exist category via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist category via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CategoryViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist categories 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CategoryViewDto>> GetAllAsync();
}
