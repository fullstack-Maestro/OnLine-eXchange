namespace Olx.Service.DTOs.Messages;

public class MessageUpdateDto
{
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public long PostId { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
}