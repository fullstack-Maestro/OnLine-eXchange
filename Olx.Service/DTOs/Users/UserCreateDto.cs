namespace Olx.Service.DTOs.Users;

public class UserCreateDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Gmail { get; set; }
    public string Hash { get; set; }
    public byte[] Salt { get; set; }
    public byte[] ProfilePicture { get; set; }
}