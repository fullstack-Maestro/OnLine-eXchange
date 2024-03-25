namespace Olx.Service.DTOs.Users;

public class UserViewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Gmail { get; set; }
    public string Hash { get; set; }
    public byte[] ProfilePicture { get; set; }
    public decimal Balance { get; set; }
}