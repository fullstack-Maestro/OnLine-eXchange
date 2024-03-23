using Olx.Service.DTOs.Messages;

namespace Olx.Service.Interfaces;

public interface IMessageService
{
    /// <summary>
    /// Create new message 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<MessageViewDto> CreateAsync(MessageCreateDto message);

    /// <summary>
    /// Update exist message
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<MessageViewDto> UpdateAsync(long id, MessageUpdateDto message);

    /// <summary>
    /// Delete exist message via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get exist message via ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MessageViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get list of exist messages
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<MessageViewDto>> GetAllAsync();
}
