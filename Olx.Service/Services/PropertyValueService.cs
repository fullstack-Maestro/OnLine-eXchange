using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.PropertyValues;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;
using System.ComponentModel.Design;

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
        await propertyValueRepository.SaveChangesAsync();

        return createPropertyValue.MapTo<PropertyValueViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPropertyValue = await propertyValueRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        existPropertyValue.IsDeleted = true;
        existPropertyValue.DeletedAt = DateTime.UtcNow;

        await propertyValueRepository.DeleteAsync(existPropertyValue);
        await propertyValueRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<PropertyValueViewDto>> GetAllAsync()
    {
        return await Task.FromResult(propertyValueRepository.SelectAllAsQueryable().MapTo<PropertyValueViewDto>());
    }

    public async Task<PropertyValueViewDto> GetByIdAsync(long id)
    {

        var existPropertyValue = await propertyValueRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

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
        await propertyValueRepository.SaveChangesAsync();

        return existPropertyValue.MapTo<PropertyValueViewDto>();
    }
}
