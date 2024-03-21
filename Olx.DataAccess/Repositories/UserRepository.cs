using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByGmail(string gmail)
    {
        return await _dbContext.User.FirstOrDefaultAsync(u => u.Gmail == gmail);
    }

    public async Task<User> GetUserById(long userId)
    {
        return await _dbContext.User.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _dbContext.User.ToListAsync();
    }

    public void AddUser(User user)
    {
        _dbContext.User.Add(user);
    }

    public void UpdateUser(User user)
    {
        _dbContext.User.Update(user);
    }

    public void DeleteUser(User user)
    {
        _dbContext.User.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
