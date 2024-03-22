using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class Message : Auditable
{
    public long SenderId { get; set; }
    public User Sender { get; set; }
    public long ReceiverId { get; set; }
    public User Receiver { get; set; }
    public long PostId { get; set; }
    public Post Post { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
}
