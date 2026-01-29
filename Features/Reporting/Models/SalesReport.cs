namespace Supermarket.API.Features.Reporting.Models;

public class SalesReport
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public int DrinkId { get; set; }
    public decimal TotalRevenue { get; set; }
    public DateTime ReportDate { get; set; }
}
