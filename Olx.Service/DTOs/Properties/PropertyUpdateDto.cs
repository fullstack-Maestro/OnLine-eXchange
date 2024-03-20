using Olx.Domain.Entities;

namespace Olx.Service.DTOs.Properties;

public class PropertyUpdateDto
{
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
}