using Olx.Domain.Entities;

namespace Olx.Service.DTOs.Messages;

public class MessageViewDto
{
    public long Id { get; set; }
    public long SenderId { get; set; }
    public User Sender { get; set; }
    public long RecieverId { get; set; }
    public User Reciever { get; set; }
    public long PostId { get; set; }
    public Post Post { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
}