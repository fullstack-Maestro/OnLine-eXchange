using Olx.Domain.Entities;

namespace Olx.Service.DTOs.PostProperties;

public class PostPropertyUpdateDto
{
    public long PostId { get; set; }
    public Post Post { get; set; }
    public long PropertyId { get; set; }
    public Property Property { get; set; }
    public long ValueId { get; set; }
    public PropertyValue Value { get; set; }
}