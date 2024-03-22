using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Posts;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;

namespace Olx.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postRepository;

        public PostsController(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostViewDto>>> GetAllPosts()
        {
            var posts = _postRepository.SelectAllAsEnumerable()
                .Where(post => !post.IsDeleted)
                .ToList();
            List<PostViewDto> postViews = new List<PostViewDto>();
            foreach (var post in posts)
            {
                postViews.Add(post.MapTo<PostViewDto>());
            }
            return Ok(postViews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewDto>> GetPostById(long id)
        {
            var post = await _postRepository.SelectByIdAsync(id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            var postView = post.MapTo<PostViewDto>();
            return Ok(postView);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(PostCreateDto createPost)
        {
            var post = createPost.MapTo<Post>();
            await _postRepository.InsertAsync(post);
            await _postRepository.SaveAsync();

            return Ok(createPost);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(long id, PostUpdateDto updatePost)
        {
            var existingPost = await _postRepository.SelectByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound("Post not found.");
            }

            existingPost.Title = updatePost.Title;
            existingPost.Description = updatePost.Description;
            existingPost.Price = updatePost.Price;
            existingPost.CategoryId = updatePost.CategoryId;
            existingPost.CityOrRegion = updatePost.CityOrRegion;
            existingPost.District = updatePost.District;
            existingPost.IsLeft = updatePost.IsLeft;

            await _postRepository.SaveAsync();

            return Ok(existingPost);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(long id)
        {
            var existingPost = await _postRepository.SelectByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound("Post not found.");
            }

            await _postRepository.DeleteAsync(existingPost);
            await _postRepository.SaveAsync();

            return NoContent();
        }
    }
}
