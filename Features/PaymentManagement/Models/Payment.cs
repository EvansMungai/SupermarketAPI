using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Features.PaymentManagement.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int SaleId { get; set; }

    public string? TransactionId { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Sale? Sale { get; set; }
}
