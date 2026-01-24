using Supermarket.API.Features.BranchManagement.Endpoints;
using Supermarket.API.Features.DrinkManagement.Endpoints;
using Supermarket.API.Features.InventoryManagement.Endpoints;
using Supermarket.API.Features.PaymentManagement.Endpoints;
using Supermarket.API.Features.SalesManagement.Endpoints;
using Supermarket.API.Features.Reporting.Endpoints;
using Supermarket.API.Features.Authentication.Endpoints;

namespace Supermarket.API.Extensions.RouteHandlers;

public static class EndpointRegistration
{
    public static void MapEndpoints(this WebApplication app)
    {
        AuthEndpoints.Map(app);
        BranchEndpoints.Map(app);
        DrinkEndpoints.Map(app);
        InventoryEndpoints.Map(app);
        PaymentEndpoints.Map(app);
        SaleEndpoints.Map(app);
        ReportEndpoints.Map(app);
    }
}
