using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Messages;
using Olx.Service.Exceptions;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

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
        await messageRepository.SaveAsync();

        return createUser.MapTo<MessageViewDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existMessage = await messageRepository.SelectByIdAsync(id)
            ?? throw new CustomException(404, "Message not found");

        existMessage.IsDeleted = true;
        existMessage.DeletedAt = DateTime.UtcNow;

        await messageRepository.DeleteAsync(existMessage);
        await messageRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<MessageViewDto>> GetAllAsync()
    {
        return await Task.FromResult(messageRepository.SelectAllAsQueryable()
            .Where(c => !c.IsDeleted)
            .MapTo<MessageViewDto>());
    }

    public async Task<MessageViewDto> GetByIdAsync(long id)
    {

        var existMessage = await messageRepository.SelectByIdAsync(id)
           ?? throw new CustomException(404, "Message not found");

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
        await messageRepository.SaveAsync();

        return existMessage.MapTo<MessageViewDto>();
    }
}
