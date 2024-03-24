using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Posts;
using Olx.Service.Exceptions;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

namespace Olx.Service.Services;

public class PostService : IPostService
{
    private readonly IRepository<Post> postRepository;

    public PostService(IRepository<Post> postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task<PostViewDto> CreateAsync(PostCreateDto post)
    {
        var existPost = await postRepository
                             .SelectAllAsQueryable()
                             .FirstOrDefaultAsync(p => p.UserId == post.UserId && p.Title == post.Title);
        if (existPost != null && existPost.IsDeleted)
            return await UpdateAsync(existPost.Id, post.MapTo<PostUpdateDto>(), true);

        if (existPost != null)
            throw new CustomException(409, "Post already exist");

        existPost = post.MapTo<Post>();
        var createUser = await postRepository.InsertAsync(existPost);
        await postRepository.SaveAsync();

        return createUser.MapTo<PostViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPost = await postRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "Post not found");

        existPost.IsDeleted = true;
        existPost.DeletedAt = DateTime.UtcNow;

        await postRepository.DeleteAsync(existPost);
        await postRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<PostViewDto>> GetAllAsync()
    {
        return await Task.FromResult(postRepository.SelectAllAsQueryable()
            .Where(c => !c.IsDeleted)
            .MapTo<PostViewDto>());
    }

    public async Task<PostViewDto> GetByIdAsync(long id)
    {
        var existPost = await postRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "Post not found");

        return existPost.MapTo<PostViewDto>();
    }

    public async Task<PostViewDto> UpdateAsync(long id, PostUpdateDto post, bool isDeleted = false)
    {
        var existPost = new Post();

        if (isDeleted)
        {
            existPost = await postRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(p => p.Id == id);
            existPost.IsDeleted = false;
        }

        existPost.Title = post.Title;
        existPost.Price = post.Price;
        existPost.UserId = post.UserId;
        existPost.District = post.District;
        existPost.UpdatedAt = DateTime.UtcNow;
        existPost.CategoryId = post.CategoryId;
        existPost.Description = post.Description;
        existPost.CityOrRegion = post.CityOrRegion;

        await postRepository.UpdateAsync(existPost);
        await postRepository.SaveAsync();

        return existPost.MapTo<PostViewDto>();
    }
}