using Olx.Service.DTOs.Properties;

namespace Olx.Service.Interfaces;

public interface IPropertyService
{
    /// <summary>
    /// Create new property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    Task<PropertyViewDto> CreateAsync(PropertyCreateDto property);

    /// <summary>
    /// Update exist property
    /// </summary>
    /// <param name="id"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    Task<PropertyViewDto> UpdateAsync(long id, PropertyUpdateDto property, bool isUsesDeleted);

    /// <summary>
    /// Delete exist property via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist property via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PropertyViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist properties
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PropertyViewDto>> GetAllAsync();
}
