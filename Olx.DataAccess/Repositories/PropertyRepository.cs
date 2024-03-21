using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _dbContext;

    public PropertyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Property> GetPropertyById(long propertyId)
    {
        return await _dbContext.Property.FindAsync(propertyId);
    }

    public async Task<List<Property>> GetAllProperties()
    {
        return await _dbContext.Property.ToListAsync();
    }

    public void AddProperty(Property property)
    {
        _dbContext.Property.Add(property);
    }

    public void UpdateProperty(Property property)
    {
        _dbContext.Property.Update(property);
    }

    public void DeleteProperty(Property property)
    {
        _dbContext.Property.Remove(property);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}