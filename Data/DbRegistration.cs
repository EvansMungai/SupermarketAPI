using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;

namespace Supermarket.API.Data;

public static class DbRegistration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<SupermarketContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35))));

        return services;
    }
}
