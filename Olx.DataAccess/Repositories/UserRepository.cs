using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserById(int userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
