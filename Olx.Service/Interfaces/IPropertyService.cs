using Olx.Service.DTOs.Properties;

namespace Olx.Service.Interfaces;

public interface IPropertyService
{
    Task<PropertyViewDto> CreateAsync(PropertyCreateDto property);
    Task<PropertyViewDto> UpdateAsync(long id, PropertyUpdateDto property);
    Task<bool> DeleteAsync(long id);
    Task<PropertyViewDto> GetByIdAsync(long id);
    Task<IEnumerable<PropertyViewDto>> GetAllAsync();
}
