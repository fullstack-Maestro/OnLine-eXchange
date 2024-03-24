namespace Olx.Service.DTOs.Users;

public class UserCreateDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Gmail { get; set; }
    public string Password { get; set; }
    public byte[] ProfilePicture { get; set; }
}