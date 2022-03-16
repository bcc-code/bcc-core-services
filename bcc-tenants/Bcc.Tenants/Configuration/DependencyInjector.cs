using Bcc.Tenants.Queries;

namespace Bcc.Tenants.Configuration;

public static class DependencyInjector
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ITenantsQueries, TenantsQueries>();
        return services;
    }
}