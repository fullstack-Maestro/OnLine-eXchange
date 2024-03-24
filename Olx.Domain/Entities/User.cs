using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class User : Auditable
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Gmail { get; set; }
    public string Password { get; set; }
    public byte[] ProfilePicture { get; set; }
    public decimal Balance { get; set; }
}