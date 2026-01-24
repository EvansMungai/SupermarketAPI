namespace Supermarket.API.Features.SalesManagement.Models;

public class PendingSaleDto
{
    public int SaleId { get; set; }
    public int BranchId { get; set; }
    public int DrinkId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public string PhoneNumber { get; set; } = null!;
}
