using Olx.Service.DTOs.PropertyValues;

namespace Olx.Service.Interfaces;

public interface IPropertyValueService
{
    /// <summary>
    /// Create new propertyValue
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    Task<PropertyValueViewDto> CreateAsync(PropertyValueCreateDto property);

    /// <summary>
    /// Update exist propertyValue
    /// </summary>
    /// <param name="id"></param>
    /// <param name="property"></param>
    /// <returns></returns>

    Task<PropertyValueViewDto> UpdateAsync(long id, PropertyValueUpdateDto property, bool isDeleted = false);

    /// <summary>
    /// Delete exist propertyValue via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist propertyValue via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PropertyValueViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist propertyValues
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PropertyValueViewDto>> GetAllAsync();
}
