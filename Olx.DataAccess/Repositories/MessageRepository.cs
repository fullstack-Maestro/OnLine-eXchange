using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;

namespace Olx.DataAccess.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _dbContext;

    public MessageRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Message> GetMessageById(long messageId)
    {
        return await _dbContext.Message.FindAsync(messageId);
    }

    public async Task<List<Message>> GetAllMessages()
    {
        return await _dbContext.Message.ToListAsync();
    }

    public void AddMessage(Message message)
    {
        _dbContext.Message.Add(message);
    }

    public void UpdateMessage(Message message)
    {
        _dbContext.Message.Update(message);
    }

    public void DeleteMessage(Message message)
    {
        _dbContext.Message.Remove(message);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}