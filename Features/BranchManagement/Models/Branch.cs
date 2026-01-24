using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Features.BranchManagement.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
