﻿using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.PostProperties;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;
using System.ComponentModel.Design;

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
            throw new Exception("Already exist");

        var createUser = await postPropertyRepository.InsertAsync(existPostProperty);
        await postPropertyRepository.SaveChangesAsync();

        return createUser.MapTo<PostPropertyViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPostProperty = await postPropertyRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        existPostProperty.IsDeleted = true;
        existPostProperty.DeletedAt = DateTime.UtcNow;

        await postPropertyRepository.DeleteAsync(existPostProperty);
        await postPropertyRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<PostPropertyViewDto>> GetAllAsync()
    {
        return await Task.FromResult(postPropertyRepository.SelectAllAsQueryable().MapTo<PostPropertyViewDto>());
    }

    public async Task<PostPropertyViewDto> GetByIdAsync(long id)
    {

        var existPostProperty = await postPropertyRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

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
        await postPropertyRepository.SaveChangesAsync();

        return existPostProperty.MapTo<PostPropertyViewDto>();
    }
}
