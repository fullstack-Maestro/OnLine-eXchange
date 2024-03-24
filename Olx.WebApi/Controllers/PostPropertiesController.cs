using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.PostProperties;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostPropertiesController : ControllerBase
    {
        private readonly IPostPropertyService _postPropertyService;

        public PostPropertiesController(IPostPropertyService postPropertyService)
        {
            _postPropertyService = postPropertyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostPropertyViewDto>>> GetAllPostProperties()
        {
            var postProperties = await _postPropertyService.GetAllAsync();
            return Ok(postProperties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostPropertyViewDto>> GetPostPropertyById(long id)
        {
            var postProperty = await _postPropertyService.GetByIdAsync(id);
            if (postProperty == null)
            {
                return NotFound("PostProperty not found.");
            }

            return Ok(postProperty);
        }

        [HttpPost]
        public async Task<ActionResult<PostPropertyViewDto>> AddPostProperty(PostPropertyCreateDto postPropertyCreateDto)
        {
            try
            {
                var addedPostProperty = await _postPropertyService.CreateAsync(postPropertyCreateDto);
                return Ok(addedPostProperty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostPropertyViewDto>> UpdatePostProperty(long id, PostPropertyUpdateDto postPropertyUpdateDto)
        {
            var updatedPostProperty = await _postPropertyService.UpdateAsync(id, postPropertyUpdateDto);
            if (updatedPostProperty == null)
            {
                return NotFound("PostProperty not found.");
            }

            return Ok(updatedPostProperty);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostProperty(long id)
        {
            var isDeleted = await _postPropertyService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("PostProperty not found.");
            }

            return NoContent();
        }
    }
}