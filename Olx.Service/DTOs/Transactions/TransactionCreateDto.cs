namespace Olx.Service.DTOs.Transactions;

public class TransactionCreateDto
{
    public long CustomerId { get; set; }
    public long SellerId { get; set; }
    public long PostId { get; set; }
}