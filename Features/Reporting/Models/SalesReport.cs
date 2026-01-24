namespace Supermarket.API.Features.Reporting.Models;

public class SalesReport
{
    public int Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string DrinkCategory { get; set; } = string.Empty;
    public decimal TotalRevenue { get; set; }
    public DateTime ReportDate { get; set; }
}
