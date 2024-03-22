using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.PropertyValues;

[Route("api/[controller]")]
[ApiController]
public class PropertyValuesController : ControllerBase
{
    private readonly IRepository<PropertyValue> _propertyValueRepository;

    public PropertyValuesController(IRepository<PropertyValue> propertyValueRepository)
    {
        _propertyValueRepository = propertyValueRepository;
    }

    [HttpGet]
    public ActionResult<List<PropertyValueViewDto>> GetAllPropertyValues()
    {
        var propertyValues = _propertyValueRepository.SelectAllAsEnumerable()
            .Where(propertyValue => !propertyValue.IsDeleted)
            .ToList();
        var propertyValueViews = propertyValues.Select(pv => new PropertyValueViewDto
        {
            Id = pv.Id,
            Value = pv.Value
        }).ToList();

        return Ok(propertyValueViews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyValueViewDto>> GetPropertyValueById(long id)
    {
        var propertyValue = await _propertyValueRepository.SelectByIdAsync(id);
        if (propertyValue == null)
        {
            return NotFound("PropertyValue not found.");
        }

        var propertyValueView = new PropertyValueViewDto
        {
            Id = propertyValue.Id,
            Value = propertyValue.Value
        };

        return Ok(propertyValueView);
    }

    [HttpPost]
    public async Task<ActionResult<PropertyValueViewDto>> AddPropertyValue(PropertyValueCreateDto propertyValueCreateDto)
    {
        var propertyValue = new PropertyValue
        {
            Value = propertyValueCreateDto.Value
        };

        var addedPropertyValue = await _propertyValueRepository.InsertAsync(propertyValue);
        await _propertyValueRepository.SaveAsync();

        var propertyValueView = new PropertyValueViewDto
        {
            Id = addedPropertyValue.Id,
            Value = addedPropertyValue.Value
        };

        return Ok(propertyValueView);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyValueViewDto>> UpdatePropertyValue(long id, PropertyValueUpdateDto propertyValueUpdateDto)
    {
        var existingPropertyValue = await _propertyValueRepository.SelectByIdAsync(id);
        if (existingPropertyValue == null)
        {
            return NotFound("PropertyValue not found.");
        }

        existingPropertyValue.Value = propertyValueUpdateDto.Value;

        var updatedPropertyValue = await _propertyValueRepository.UpdateAsync(existingPropertyValue);
        await _propertyValueRepository.SaveAsync();

        var propertyValueView = new PropertyValueViewDto
        {
            Id = updatedPropertyValue.Id,
            Value = updatedPropertyValue.Value
        };

        return Ok(propertyValueView);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePropertyValue(long id)
    {
        var existingPropertyValue = await _propertyValueRepository.SelectByIdAsync(id);
        if (existingPropertyValue == null)
        {
            return NotFound("PropertyValue not found.");
        }

        await _propertyValueRepository.DeleteAsync(existingPropertyValue);
        await _propertyValueRepository.SaveAsync();

        return NoContent();
    }
}