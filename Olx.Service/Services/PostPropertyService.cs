using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.PostProperties;
using Olx.Service.Exceptions;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

namespace Olx.Service.Services;

public class PostPropertyService : IPostPropertyService
{
    private readonly IRepository<PostProperty> postPropertyRepository;
    public PostPropertyService(IRepository<PostProperty> postPropertyRepository)
    {
        this.postPropertyRepository = postPropertyRepository;
    }
    public async Task<PostPropertyViewDto> CreateAsync(PostPropertyCreateDto postProperty)
    {
        var existPostProperty = await postPropertyRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(p => p.PostId == postProperty.PostId && p.PropertyId == postProperty.PropertyId);
        if (existPostProperty != null && existPostProperty.IsDeleted)
            return await UpdateAsync(existPostProperty.Id, postProperty.MapTo<PostPropertyUpdateDto>(), true);

        if (existPostProperty != null)
            throw new CustomException(409, "PostProperty already exist");

        var createUser = await postPropertyRepository.InsertAsync(existPostProperty);
        await postPropertyRepository.SaveAsync();

        return createUser.MapTo<PostPropertyViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPostProperty = await postPropertyRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "PostProperty not found");

        existPostProperty.IsDeleted = true;
        existPostProperty.DeletedAt = DateTime.UtcNow;

        await postPropertyRepository.DeleteAsync(existPostProperty);
        await postPropertyRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<PostPropertyViewDto>> GetAllAsync()
    {
        return await Task.FromResult(postPropertyRepository.SelectAllAsQueryable().MapTo<PostPropertyViewDto>());
    }

    public async Task<PostPropertyViewDto> GetByIdAsync(long id)
    {
        var existPostProperty = await postPropertyRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "PostProperty not found");

        return existPostProperty.MapTo<PostPropertyViewDto>();
    }

    public async Task<PostPropertyViewDto> UpdateAsync(long id, PostPropertyUpdateDto postProperty, bool isDeleted = false)
    {
        var existPostProperty = new PostProperty();

        if (isDeleted)
        {
            existPostProperty = await postPropertyRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(u => u.Id == id);
            existPostProperty.IsDeleted = false;
        }

        existPostProperty.PostId = postProperty.PostId;
        existPostProperty.PropertyId = postProperty.PropertyId;
        existPostProperty.ValueId = postProperty.ValueId;
        existPostProperty.UpdatedAt = DateTime.UtcNow;

        await postPropertyRepository.UpdateAsync(existPostProperty);
        await postPropertyRepository.SaveAsync();

        return existPostProperty.MapTo<PostPropertyViewDto>();
    }
}
