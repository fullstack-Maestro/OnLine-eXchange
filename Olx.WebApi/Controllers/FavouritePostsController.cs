using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var favouritePost = await _favouritePostService.GetByIdAsync(id);
            if (favouritePost == null)
            {
                return NotFound("Favorite Post not found.");
            }

            return Ok(favouritePost);
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

            return NoContent();
        }
    }
}