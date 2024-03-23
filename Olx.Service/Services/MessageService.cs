using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Categories;
using Olx.Service.DTOs.Messages;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;
using System.ComponentModel.Design;

namespace Olx.Service.Services;

public class MessageService : IMessageService
{
    private readonly IRepository<Message> messageRepository;
    public MessageService(IRepository<Message> messageRepository)
    {
        this.messageRepository = messageRepository;
    }
    public async Task<MessageViewDto> CreateAsync(MessageCreateDto message)
    {
        var createUser = await messageRepository.InsertAsync(message.MapTo<Message>());
        await messageRepository.SaveChangesAsync();

        return createUser.MapTo<MessageViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existMessage = await messageRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        existMessage.IsDeleted = true;
        existMessage.DeletedAt = DateTime.UtcNow;

        await messageRepository.DeleteAsync(existMessage);
        await messageRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<MessageViewDto>> GetAllAsync()
    {
        return await Task.FromResult(messageRepository.SelectAllAsQueryable().MapTo<MessageViewDto>());
    }

    public async Task<MessageViewDto> GetByIdAsync(long id)
    {

        var existMessage = await messageRepository.SelectByIdAsync(id)
            ?? throw new Exception("Not found");

        return existMessage.MapTo<MessageViewDto>();
    }

    public async Task<MessageViewDto> UpdateAsync(long id, MessageUpdateDto message, bool isDeleted = false)
    {
        var existMessage = new Message();

        if (isDeleted)
        {
            existMessage = await messageRepository
                              .SelectAllAsQueryable()
                              .FirstOrDefaultAsync(m => m.Id == id);
            existMessage.IsDeleted = false;
        }

        existMessage.PostId = message.PostId;
        existMessage.Content = message.Content;
        existMessage.SenderId = message.SenderId;
        existMessage.ReceiverId = message.ReceiverId;
        existMessage.UpdatedAt = DateTime.UtcNow;

        await messageRepository.UpdateAsync(existMessage);
        await messageRepository.SaveChangesAsync();

        return existMessage.MapTo<MessageViewDto>();
    }
}
