using Olx.Service.DTOs.Messages;

namespace Olx.Service.Interfaces;

public interface IMessageService
{
    Task<MessageViewDto> CreateAsync(MessageCreateDto message);
    Task<MessageViewDto> UpdateAsync(long id, MessageUpdateDto message);
    Task<bool> DeleteAsync(long id);
    Task<MessageViewDto> GetByIdAsync(long id);
    Task<IEnumerable<MessageViewDto>> GetAllAsync();
}
