namespace Olx.Service.DTOs.Users;

public class UserUpdateDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Gmail { get; set; }
    public string Password { get; set; }
    public byte[] ProfilePicture { get; set; }
    public bool IsVip { get; set; } = false;
}