namespace Olx.Service.DTOs.Transactions;

public class TransactionViewDto
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string CustomerName { get; set; }
    public long SellerId { get; set; }
    public string SellerName { get; set; }
    public long PostId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}