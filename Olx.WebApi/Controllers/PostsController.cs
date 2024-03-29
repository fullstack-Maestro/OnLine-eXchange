﻿using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.Posts;
using Olx.Service.Interfaces;

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
            try
            {
                var post = await _postService.GetByIdAsync(id);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
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

            return Ok("Post deleted.");
        }
    }
}