using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.PropertyValues;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyValuesController : ControllerBase
    {
        private readonly IPropertyValueService _propertyValueService;

        public PropertyValuesController(IPropertyValueService propertyValueService)
        {
            _propertyValueService = propertyValueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyValueViewDto>>> GetAllPropertyValues()
        {
            var propertyValues = await _propertyValueService.GetAllAsync();
            return Ok(propertyValues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyValueViewDto>> GetPropertyValueById(long id)
        {
            var propertyValue = await _propertyValueService.GetByIdAsync(id);
            if (propertyValue == null)
            {
                return NotFound("PropertyValue not found.");
            }

            return Ok(propertyValue);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyValueViewDto>> AddPropertyValue(PropertyValueCreateDto propertyValueCreateDto)
        {
            try
            {
                var addedPropertyValue = await _propertyValueService.CreateAsync(propertyValueCreateDto);
                return Ok(addedPropertyValue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PropertyValueViewDto>> UpdatePropertyValue(long id, PropertyValueUpdateDto propertyValueUpdateDto)
        {
            var updatedPropertyValue = await _propertyValueService.UpdateAsync(id, propertyValueUpdateDto);
            if (updatedPropertyValue == null)
            {
                return NotFound("PropertyValue not found.");
            }

            return Ok(updatedPropertyValue);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePropertyValue(long id)
        {
            var isDeleted = await _propertyValueService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("PropertyValue not found.");
            }

            return NoContent();
        }
    }
}