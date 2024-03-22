using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.FavouritePosts;


namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritePostsController : ControllerBase
{
    private readonly IRepository<FavouritePost> _favouritePostRepository;

    public FavouritePostsController(IRepository<FavouritePost> favouritePostRepository)
    {
        _favouritePostRepository = favouritePostRepository;
    }

    [HttpGet]
    public ActionResult<List<FavouritePostViewDto>> GetAllFavouritePosts()
    {
        var favouritePosts = _favouritePostRepository.SelectAllAsEnumerable()
            .Where(favouritePost => !favouritePost.IsDeleted)
            .ToList();

        var favouritePostViews = favouritePosts.Select(fp => new FavouritePostViewDto
        {
            UserId = fp.UserId,
            PostId = fp.PostId
        }).ToList();

        return Ok(favouritePostViews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FavouritePostViewDto>> GetFavouritePostById(long id)
    {
        var favouritePost = await _favouritePostRepository.SelectByIdAsync(id);
        if (favouritePost == null)
        {
            return NotFound("FavouritePost not found.");
        }

        var favouritePostView = new FavouritePostViewDto
        {
            UserId = favouritePost.UserId,
            PostId = favouritePost.PostId
        };

        return Ok(favouritePostView);
    }

    [HttpPost]
    public async Task<ActionResult<FavouritePostViewDto>> AddFavouritePost(FavouritePostCreateDto favouritePostCreateDto)
    {
        var favouritePost = new FavouritePost
        {
            UserId = favouritePostCreateDto.UserId,
            PostId = favouritePostCreateDto.PostId
        };

        var addedFavouritePost = await _favouritePostRepository.InsertAsync(favouritePost);
        await _favouritePostRepository.SaveAsync();

        var favouritePostView = new FavouritePostViewDto
        {
            UserId = addedFavouritePost.UserId,
            PostId = addedFavouritePost.PostId
        };

        return Ok(favouritePostView);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFavouritePost(long id)
    {
        var favouritePost = await _favouritePostRepository.SelectByIdAsync(id);
        if (favouritePost == null)
        {
            return NotFound("FavouritePost not found.");
        }

        await _favouritePostRepository.DeleteAsync(favouritePost);
        await _favouritePostRepository.SaveAsync();

        return NoContent();
    }
}