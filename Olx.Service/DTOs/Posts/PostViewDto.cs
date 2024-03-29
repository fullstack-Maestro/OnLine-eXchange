namespace Olx.Service.DTOs.Posts;

public class PostViewDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public string CityOrRegion { get; set; }
    public string District { get; set; }
    public bool IsLeft { get; set; } = true;
}