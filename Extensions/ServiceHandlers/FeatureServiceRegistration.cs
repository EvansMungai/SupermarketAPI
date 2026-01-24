using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Supermarket.API.Extensions.ServiceHandlers;

public static class FeatureServiceRegistration
{
    public static void RegisterFeatureServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Register Repositories
        var repositories = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Repository") && t.IsClass && !t.IsAbstract);

        foreach (var repository in repositories)
        {
            var interfaceType = repository.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{repository.Name}");

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, repository);
            }
        }

        // Register Services
        var appServices = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract);

        foreach (var service in appServices)
        {
            var interfaceType = service.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{service.Name}");

            if(interfaceType != null)
            {
                // Register with its abstraction
                services.AddScoped(interfaceType, service);
            } else
            {
                // Register as self
                services.AddScoped(service);
            }
        }
    }
}
