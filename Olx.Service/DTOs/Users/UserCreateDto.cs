namespace Olx.Service.DTOs.Users;

public class UserCreateDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Gmail { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public bool IsVip = false;
}