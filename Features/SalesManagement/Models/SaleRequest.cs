namespace Supermarket.API.Features.SalesManagement.Models;

public record SaleRequest(int BranchId, string UserId, int DrinkId, int Quantity, string PhoneNumber);
