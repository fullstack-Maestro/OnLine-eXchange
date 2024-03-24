using Olx.DataAccess.Contexts;
using Olx.Domain.Entities;

namespace Olx.DataAccess.SeedData;

public class SeedData
{
    private readonly AppDbContext _context;

    public SeedData(AppDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        if (!_context.Category.Any())
        {
            // Seed categories
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Home Appliances" },
                // Add more categories as needed
            };

            _context.Category.AddRange(categories);
            _context.SaveChanges();
        }

        if (!_context.Property.Any())
        {
            // Seed properties
            var properties = new List<Property>
            {
                new Property { Id = 1, Name = "Color", CategoryId = 1 },
                new Property { Id = 2, Name = "Size", CategoryId = 2 },
                new Property { Id = 3, Name = "Power Consumption", CategoryId = 3 },
                // Add more properties as needed
            };

            _context.Property.AddRange(properties);
            _context.SaveChanges();
        }

        if (!_context.PropertyValue.Any())
        {
            // Seed property values
            var propertyValues = new List<PropertyValue>
            {
                new PropertyValue { Id = 1, Value = "Black" },
                new PropertyValue { Id = 2, Value = "Medium" },
                new PropertyValue { Id = 3, Value = "Low" },
                // Add more property values as needed
            };

            _context.PropertyValue.AddRange(propertyValues);
            _context.SaveChanges();
        }

    }
}