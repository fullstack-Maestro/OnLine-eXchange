using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> GetCategoryById(int categoryId)
    {
        return await _dbContext.Categories.FindAsync(categoryId);
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public void AddCategory(Category category)
    {
        _dbContext.Categories.Add(category);
    }

    public void UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
    }

    public void DeleteCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}