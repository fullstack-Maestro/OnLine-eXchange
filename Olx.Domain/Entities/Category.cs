using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class Category : Auditable
{
    public string Name { get; set; }
    public long? ParentId { get; set; }
}
