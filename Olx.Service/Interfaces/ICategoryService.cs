using Olx.Service.DTOs.Categories;

namespace Olx.Service.Interfaces;

public interface ICategoryService
{
    Task<CategoryViewDto> CreateAsync(CategoryCreateDto category);
    Task<CategoryViewDto> UpdateAsync(long id, CategoryUpdateDto category,bool isDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<CategoryViewDto> GetByIdAsync(long id);
    Task<IEnumerable<CategoryViewDto>> GetAllAsync();
}
