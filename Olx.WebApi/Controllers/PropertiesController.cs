using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Properties;
using Olx.Service.Extentions;




namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly IRepository<Property> _propertyRepository;

    public PropertiesController(IRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    [HttpGet]
    public ActionResult<List<PropertyViewDto>> GetAllProperties()
    {
        var properties = _propertyRepository.SelectAllAsEnumerable()
            .Where(property => !property.IsDeleted)
            .ToList();
        var propertyViewDtos = properties.Select(property => property.MapTo<PropertyViewDto>()).ToList();
        return Ok(propertyViewDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyViewDto>> GetPropertyById(long id)
    {
        var property = await _propertyRepository.SelectByIdAsync(id);
        if (property == null)
        {
            return NotFound("Property not found.");
        }

        var propertyDto = property.MapTo<PropertyViewDto>();
        return Ok(propertyDto);
    }

    [HttpPost]
    public async Task<ActionResult<PropertyViewDto>> AddProperty(PropertyCreateDto propertyCreateDto)
    {
        var property = propertyCreateDto.MapTo<Property>();
        var addedProperty = await _propertyRepository.InsertAsync(property);
        await _propertyRepository.SaveAsync();

        var propertyDto = addedProperty.MapTo<PropertyViewDto>();
        return Ok(propertyDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyViewDto>> UpdateProperty(long id, PropertyUpdateDto propertyUpdateDto)
    {
        var existingProperty = await _propertyRepository.SelectByIdAsync(id);
        if (existingProperty == null)
        {
            return NotFound("Property not found.");
        }

        existingProperty.Name = propertyUpdateDto.Name;

        var updatedProperty = await _propertyRepository.UpdateAsync(existingProperty);
        await _propertyRepository.SaveAsync();

        var propertyDto = updatedProperty.MapTo<PropertyViewDto>();
        return Ok(propertyDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProperty(long id)
    {
        var existingProperty = await _propertyRepository.SelectByIdAsync(id);
        if (existingProperty == null)
        {
            return NotFound("Property not found.");
        }

        await _propertyRepository.DeleteAsync(existingProperty);
        await _propertyRepository.SaveAsync();

        return NoContent();
    }
}