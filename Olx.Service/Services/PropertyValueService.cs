using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.PropertyValues;
using Olx.Service.Exceptions;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

namespace Olx.Service.Services;

public class PropertyValueService : IPropertyValueService
{
    private readonly IRepository<PropertyValue> propertyValueRepository;
    public PropertyValueService(IRepository<PropertyValue> propertyValueRepository)
    {
        this.propertyValueRepository = propertyValueRepository;
    }
    public async Task<PropertyValueViewDto> CreateAsync(PropertyValueCreateDto propertyValue)
    {
        var createPropertyValue = await propertyValueRepository.InsertAsync(propertyValue.MapTo<PropertyValue>());
        await propertyValueRepository.SaveAsync();

        return createPropertyValue.MapTo<PropertyValueViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPropertyValue = await propertyValueRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "Value not found");

        existPropertyValue.IsDeleted = true;
        existPropertyValue.DeletedAt = DateTime.UtcNow;

        await propertyValueRepository.DeleteAsync(existPropertyValue);
        await propertyValueRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<PropertyValueViewDto>> GetAllAsync()
    {
        return await Task.FromResult(propertyValueRepository.SelectAllAsQueryable().MapTo<PropertyValueViewDto>());
    }

    public async Task<PropertyValueViewDto> GetByIdAsync(long id)
    {
        var existPropertyValue = await propertyValueRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "Value not found");

        return existPropertyValue.MapTo<PropertyValueViewDto>();
    }

    public async Task<PropertyValueViewDto> UpdateAsync(long id, PropertyValueUpdateDto propertyValue, bool isDeleted = false)
    {
        var existPropertyValue = new PropertyValue();

        if (isDeleted)
        {
            existPropertyValue = await propertyValueRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(u => u.Id == id);
            existPropertyValue.IsDeleted = false;
        }

        existPropertyValue.Value = propertyValue.Value;
        existPropertyValue.UpdatedAt = DateTime.UtcNow;

        await propertyValueRepository.UpdateAsync(existPropertyValue);
        await propertyValueRepository.SaveAsync();

        return existPropertyValue.MapTo<PropertyValueViewDto>();
    }
}
