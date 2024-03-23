using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olx.Service.DTOs.Posts;
using Olx.Service.Interfaces;
using Olx.Service.DTOs.PostProperties;
using Olx.Service.Services;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostViewDto>>> GetAllPosts()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewDto>> GetPostById(long id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostViewDto>> AddPost(PostCreateDto createPost)
        {
            try
            {
                var addedPost = await _postService.CreateAsync(createPost);
                return Ok(addedPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostViewDto>> UpdatePost(long id, PostUpdateDto updatePost)
        {
            var updatedPost = await _postService.UpdateAsync(id, updatePost);
            if (updatedPost == null)
            {
                return NotFound("Post not found.");
            }

            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(long id)
        {
            var isDeleted = await _postService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("Post not found.");
            }

            return NoContent();
        }
    }
}