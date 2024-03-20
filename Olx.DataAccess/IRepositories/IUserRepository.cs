using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void DeleteUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task SaveChangesAsync();
        void UpdateUser(User user);
    }
}