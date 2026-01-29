namespace Supermarket.API.Features.InventoryManagement.Models;

public class InventoryResponseDto
{
    public int InventoryId { get; set; }

    public int? BranchId { get; set; }

    public int? DrinkId { get; set; }

    public int? StockQuantity { get; set; }
}
