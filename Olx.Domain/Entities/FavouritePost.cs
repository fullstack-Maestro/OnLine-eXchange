using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class FavouritePost : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long PostId { get; set; }
    public Post Post { get; set; }
}
