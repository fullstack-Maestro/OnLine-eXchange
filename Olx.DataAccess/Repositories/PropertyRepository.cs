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

    public async Task<Property> GetPropertyById(int propertyId)
    {
        return await _dbContext.Properties.FindAsync(propertyId);
    }

    public async Task<List<Property>> GetAllProperties()
    {
        return await _dbContext.Properties.ToListAsync();
    }

    public void AddProperty(Property property)
    {
        _dbContext.Properties.Add(property);
    }

    public void UpdateProperty(Property property)
    {
        _dbContext.Properties.Update(property);
    }

    public void DeleteProperty(Property property)
    {
        _dbContext.Properties.Remove(property);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}