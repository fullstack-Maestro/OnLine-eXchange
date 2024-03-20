using Olx.Domain.Entities;

namespace Olx.Service.DTOs.Posts;

public class PostUpdateDto
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public string CityOrRegion { get; set; }
    public string District { get; set; }
    public bool IsLeft = true;
}