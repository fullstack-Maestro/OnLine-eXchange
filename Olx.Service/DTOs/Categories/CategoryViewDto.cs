namespace Olx.Service.DTOs.Categories;

public class CategoryViewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long? ParentId { get; set; }
}