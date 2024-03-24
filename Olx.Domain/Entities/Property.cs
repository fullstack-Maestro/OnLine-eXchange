using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class Property : Auditable
{
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
}