﻿using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.Categories;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewDto>> GetCategoryById(long id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryViewDto>> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var addedCategory = await _categoryService.CreateAsync(categoryCreateDto);
                return Ok(addedCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryViewDto>> UpdateCategory(long id, CategoryUpdateDto categoryUpdateDto)
        {
            var updatedCategory = await _categoryService.UpdateAsync(id, categoryUpdateDto);
            if (updatedCategory == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(long id)
        {
            var isDeleted = await _categoryService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("Category not found.");
            }
            else
            {
                return Ok("Successfully Deleted!");
            }
        }
    }
}