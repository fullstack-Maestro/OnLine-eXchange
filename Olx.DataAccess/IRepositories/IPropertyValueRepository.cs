using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IPropertyValueRepository
    {
        void AddPropertyValue(PropertyValue propertyValue);
        void DeletePropertyValue(PropertyValue propertyValue);
        Task<List<PropertyValue>> GetAllPropertyValues();
        Task<PropertyValue> GetPropertyValueById(int propertyValueId);
        Task SaveChangesAsync();
        void UpdatePropertyValue(PropertyValue propertyValue);
    }
}