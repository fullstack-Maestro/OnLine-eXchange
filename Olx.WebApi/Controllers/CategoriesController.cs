using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Categories;
using Olx.Service.Extentions;


namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoriesController(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public ActionResult<List<CategoryViewDto>> GetAllCategories()
    {
        var categories = _categoryRepository.SelectAllAsEnumerable()
            .Where(category => !category.IsDeleted)
            .ToList();

        var categoryDtos = categories.Select(category => category.MapTo<CategoryViewDto>()).ToList();

        return Ok(categoryDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryViewDto>> GetCategoryById(long id)
    {
        var category = await _categoryRepository.SelectByIdAsync(id);
        if (category == null)
        {
            return NotFound("Category not found.");
        }

        var categoryDto = category.MapTo<CategoryViewDto>();
        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryViewDto>> AddCategory(CategoryCreateDto categoryCreateDto)
    {
        var category = categoryCreateDto.MapTo<Category>();
        var addedCategory = await _categoryRepository.InsertAsync(category);
        await _categoryRepository.SaveAsync();

        var categoryDto = addedCategory.MapTo<CategoryViewDto>();
        return Ok(categoryDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryViewDto>> UpdateCategory(long id, CategoryUpdateDto categoryUpdateDto)
    {
        var existingCategory = await _categoryRepository.SelectByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound("Category not found.");
        }

        existingCategory.Name = categoryUpdateDto.Name;
        existingCategory.ParentId = categoryUpdateDto.ParentId;

        var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
        await _categoryRepository.SaveAsync();

        var categoryDto = updatedCategory.MapTo<CategoryViewDto>();
        return Ok(categoryDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(long id)
    {
        var existingCategory = await _categoryRepository.SelectByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound("Category not found.");
        }

        await _categoryRepository.DeleteAsync(existingCategory);
        await _categoryRepository.SaveAsync();

        return NoContent();
    }
}
