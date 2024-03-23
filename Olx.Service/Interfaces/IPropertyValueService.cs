using Olx.Service.DTOs.PropertyValues;

namespace Olx.Service.Interfaces;

public interface IPropertyValueService
{
    Task<PropertyValueViewDto> CreateAsync(PropertyValueCreateDto property);
    Task<PropertyValueViewDto> UpdateAsync(long id, PropertyValueUpdateDto property, bool isDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<PropertyValueViewDto> GetByIdAsync(long id);
    Task<IEnumerable<PropertyValueViewDto>> GetAllAsync();
}
