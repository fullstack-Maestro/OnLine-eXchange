using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.FavouritePosts;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritePostsController : ControllerBase
    {
        private readonly IFavouritePostService _favouritePostService;

        public FavouritePostsController(IFavouritePostService favouritePostService)
        {
            _favouritePostService = favouritePostService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouritePostViewDto>>> GetAllFavouritePosts()
        {
            var favouritePosts = await _favouritePostService.GetAllAsync();
            return Ok(favouritePosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FavouritePostViewDto>> GetFavouritePostById(long id)
        {
            try
            {
                var favouritePost = await _favouritePostService.GetByIdAsync(id);

                return Ok(favouritePost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<FavouritePostViewDto>> AddFavouritePost(FavouritePostCreateDto favouritePostCreateDto)
        {
            try
            {
                var addedFavouritePost = await _favouritePostService.CreateAsync(favouritePostCreateDto);
                return Ok(addedFavouritePost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFavouritePost(long id)
        {
            var isDeleted = await _favouritePostService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("FavouritePost not found.");
            }

            return Ok("Favourite Post deleted.");
        }
    }
}