using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;
using System.ComponentModel.Design;

namespace Olx.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository;
    public UserService(IRepository<User> userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<UserViewDto> CreateAsync(UserCreateDto user)
    {
        var existUser = await userRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(u => u.Gmail == user.Gmail);
        if (existUser != null && existUser.IsDeleted)
            return await UpdateAsync(existUser.Id, user.MapTo<UserUpdateDto>(), true);

        if (existUser != null)
            throw new Exception("Already exist");

        var createUser = await userRepository.InsertAsync(existUser);
        await userRepository.SaveChangesAsync();

        return createUser.MapTo<UserViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await userRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        existUser.IsDeleted = true;
        existUser.DeletedAt = DateTime.UtcNow;

        await userRepository.DeleteAsync(existUser);
        await userRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<UserViewDto>> GetAllAsync()
    {
        return await Task.FromResult(userRepository.SelectAllAsQueryable().MapTo<UserViewDto>());
    }

    public async Task<UserViewDto> GetByIdAsync(long id)
    {

        var existUser = await userRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        return existUser.MapTo<UserViewDto>();
    }

    public async Task<UserViewDto> UpdateAsync(long id, UserUpdateDto user, bool isDeleted = false)
    {
        var existUser = new User();

        if(isDeleted)
        {
            existUser = await userRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(u => u.Id == id);
            existUser.IsDeleted = false;
        }

        existUser.Name = user.Name;
        existUser.Gmail = user.Gmail;
        existUser.IsVip = user.IsVip;
        existUser.Password = user.Password;
        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.PhoneNumber = user.PhoneNumber;
        existUser.ProfilePicture = user.ProfilePicture;

        await userRepository.UpdateAsync(existUser);
        await userRepository.SaveChangesAsync();

        return existUser.MapTo<UserViewDto>();
    }
}
