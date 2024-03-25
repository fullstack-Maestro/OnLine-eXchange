using Olx.Domain.Commons;

namespace Olx.Domain.Entities;

public class Transaction : Auditable
{
    public long CustomerId { get; set; }
    public User Customer { get; set; }
    public long SellerId { get; set; }
    public User Seller { get; set; }
    public long PostId { get; set; }
    public Post Post { get; set; }
    public decimal Amount { get; set; }
}