using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class PropertyValueRepository : IPropertyValueRepository
{
    private readonly AppDbContext _dbContext;

    public PropertyValueRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PropertyValue> GetPropertyValueById(long propertyValueId)
    {
        return await _dbContext.PropertyValue.FindAsync(propertyValueId);
    }

    public async Task<List<PropertyValue>> GetAllPropertyValues()
    {
        return await _dbContext.PropertyValue.ToListAsync();
    }

    public void AddPropertyValue(PropertyValue propertyValue)
    {
        _dbContext.PropertyValue.Add(propertyValue);
    }

    public void UpdatePropertyValue(PropertyValue propertyValue)
    {
        _dbContext.PropertyValue.Update(propertyValue);
    }

    public void DeletePropertyValue(PropertyValue propertyValue)
    {
        _dbContext.PropertyValue.Remove(propertyValue);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}