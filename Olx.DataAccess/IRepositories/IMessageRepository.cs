using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<List<Message>> GetAllMessages();
        Task<Message> GetMessageById(int messageId);
        Task SaveChangesAsync();
        void UpdateMessage(Message message);
    }
}