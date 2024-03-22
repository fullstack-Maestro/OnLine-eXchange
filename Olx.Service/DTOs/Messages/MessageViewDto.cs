namespace Olx.Service.DTOs.Messages;

public class MessageViewDto
{
    public long Id { get; set; }
    public long SenderId { get; set; }
    public long RecieverId { get; set; }
    public long PostId { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
}