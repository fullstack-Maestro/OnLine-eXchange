using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IPostPropertyRepository
    {
        void AddPostProperty(PostProperty postProperty);
        void DeletePostProperty(PostProperty postProperty);
        Task<List<PostProperty>> GetAllPostProperties();
        Task<PostProperty> GetPostPropertyById(long postPropertyId);
        Task SaveChangesAsync();
        void UpdatePostProperty(PostProperty postProperty);
    }
}