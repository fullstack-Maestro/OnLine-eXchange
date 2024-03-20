using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task SaveChangesAsync();
        void UpdateCategory(Category category);
    }
}