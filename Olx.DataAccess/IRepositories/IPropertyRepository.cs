using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IPropertyRepository
    {
        void AddProperty(Property property);
        void DeleteProperty(Property property);
        Task<List<Property>> GetAllProperties();
        Task<Property> GetPropertyById(long propertyId);
        Task SaveChangesAsync();
        void UpdateProperty(Property property);
    }
}