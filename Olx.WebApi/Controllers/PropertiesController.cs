using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.Properties;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyViewDto>>> GetAllProperties()
        {
            var properties = await _propertyService.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyViewDto>> GetPropertyById(long id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound("Property not found.");
            }

            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyViewDto>> AddProperty(PropertyCreateDto propertyCreateDto)
        {
            try
            {
                var addedProperty = await _propertyService.CreateAsync(propertyCreateDto);
                return Ok(addedProperty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PropertyViewDto>> UpdateProperty(long id, PropertyUpdateDto propertyUpdateDto)
        {
            var updatedProperty = await _propertyService.UpdateAsync(id, propertyUpdateDto);
            if (updatedProperty == null)
            {
                return NotFound("Property not found.");
            }

            return Ok(updatedProperty);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProperty(long id)
        {
            var isDeleted = await _propertyService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("Property not found.");
            }

            return NoContent();
        }
    }
}