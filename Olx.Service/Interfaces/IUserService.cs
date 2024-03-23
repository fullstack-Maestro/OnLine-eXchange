using Olx.Service.DTOs.Users;

namespace Olx.Service.Interfaces;

public interface IUserService
{
    Task<UserViewDto> CreateAsync(UserCreateDto user);
    Task<UserViewDto> UpdateAsync(long id, UserUpdateDto user, bool isDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<UserViewDto> GetByIdAsync(long id);
    Task<IEnumerable<UserViewDto>> GetAllAsync();
}