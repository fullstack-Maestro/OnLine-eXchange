using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Categories;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

namespace Olx.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> categoryRepository;
    public CategoryService(IRepository<Category> categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public async Task<CategoryViewDto> CreateAsync(CategoryCreateDto category)
    {
        var existCategory = await categoryRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(c => c.Name == category.Name);
        if (existCategory != null && existCategory.IsDeleted)
            return await UpdateAsync(existCategory.Id, category.MapTo<CategoryUpdateDto>(), true);

        if (existCategory != null)
            throw new Exception("Already exist");

        var createUser = await categoryRepository.InsertAsync(existCategory);
        await categoryRepository.SaveAsync();

        return createUser.MapTo<CategoryViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCategory = await categoryRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        existCategory.IsDeleted = true;
        existCategory.DeletedAt = DateTime.UtcNow;

        await categoryRepository.DeleteAsync(existCategory);
        await categoryRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<CategoryViewDto>> GetAllAsync()
    {
        return await Task.FromResult(categoryRepository.SelectAllAsQueryable().MapTo<CategoryViewDto>());
    }

    public async Task<CategoryViewDto> GetByIdAsync(long id)
    {

        var existCategory = await categoryRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        return existCategory.MapTo<CategoryViewDto>();
    }

    public async Task<CategoryViewDto> UpdateAsync(long id, CategoryUpdateDto category, bool isDeleted = false)
    {
        var existCategory = new Category();

        if (isDeleted)
        {
            existCategory = await categoryRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(c => c.Id == id);
            existCategory.IsDeleted = false;
        }

        existCategory.Name = category.Name;
        existCategory.ParentId = category.ParentId;
        existCategory.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.UpdateAsync(existCategory);
        await categoryRepository.SaveAsync();

        return existCategory.MapTo<CategoryViewDto>();
    }
}
