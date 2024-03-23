using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.PostProperties;


namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostPropertiesController : ControllerBase
{
    private readonly IRepository<PostProperty> _postPropertyRepository;

    public PostPropertiesController(IRepository<PostProperty> postPropertyRepository)
    {
        _postPropertyRepository = postPropertyRepository;
    }

    [HttpGet]
    public ActionResult<List<PostPropertyViewDto>> GetAllPostProperties()
    {
        var postProperties = _postPropertyRepository.SelectAllAsEnumerable()
            .Where(postProperty => !postProperty.IsDeleted)
            .ToList();
        var postPropertyViews = postProperties.Select(pp => new PostPropertyViewDto
        {
            Id = pp.Id,
            PostId = pp.PostId,
            PropertyId = pp.PropertyId,
            ValueId = pp.ValueId
        }).ToList();

        return Ok(postPropertyViews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostPropertyViewDto>> GetPostPropertyById(long id)
    {
        var postProperty = await _postPropertyRepository.SelectByIdAsync(id);
        if (postProperty == null)
        {
            return NotFound("PostProperty not found.");
        }

        var postPropertyView = new PostPropertyViewDto
        {
            Id = postProperty.Id,
            PostId = postProperty.PostId,
            PropertyId = postProperty.PropertyId,
            ValueId = postProperty.ValueId
        };

        return Ok(postPropertyView);
    }

    [HttpPost]
    public async Task<ActionResult<PostPropertyViewDto>> AddPostProperty(PostPropertyCreateDto postPropertyCreateDto)
    {
        var postProperty = new PostProperty
        {
            PostId = postPropertyCreateDto.PostId,
            PropertyId = postPropertyCreateDto.PropertyId,
            ValueId = postPropertyCreateDto.ValueId
        };

        var addedPostProperty = await _postPropertyRepository.InsertAsync(postProperty);
        await _postPropertyRepository.SaveAsync();

        var postPropertyView = new PostPropertyViewDto
        {
            Id = addedPostProperty.Id,
            PostId = addedPostProperty.PostId,
            PropertyId = addedPostProperty.PropertyId,
            ValueId = addedPostProperty.ValueId
        };

        return Ok(postPropertyView);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostPropertyViewDto>> UpdatePostProperty(long id, PostPropertyUpdateDto postPropertyUpdateDto)
    {
        var existingPostProperty = await _postPropertyRepository.SelectByIdAsync(id);
        if (existingPostProperty == null)
        {
            return NotFound("PostProperty not found.");
        }

        existingPostProperty.PostId = postPropertyUpdateDto.PostId;
        existingPostProperty.PropertyId = postPropertyUpdateDto.PropertyId;
        existingPostProperty.ValueId = postPropertyUpdateDto.ValueId;

        var updatedPostProperty = await _postPropertyRepository.UpdateAsync(existingPostProperty);
        await _postPropertyRepository.SaveAsync();

        var postPropertyView = new PostPropertyViewDto
        {
            Id = updatedPostProperty.Id,
            PostId = updatedPostProperty.PostId,
            PropertyId = updatedPostProperty.PropertyId,
            ValueId = updatedPostProperty.ValueId
        };

        return Ok(postPropertyView);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePostProperty(long id)
    {
        var existingPostProperty = await _postPropertyRepository.SelectByIdAsync(id);
        if (existingPostProperty == null)
        {
            return NotFound("PostProperty not found.");
        }

        await _postPropertyRepository.DeleteAsync(existingPostProperty);
        await _postPropertyRepository.SaveAsync();

        return NoContent();
    }
}