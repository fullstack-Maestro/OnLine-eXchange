using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class PropertyValue : Auditable
{
    public string Value { get; set; }
}