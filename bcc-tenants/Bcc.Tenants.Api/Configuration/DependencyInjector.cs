using Bcc.Tenants.Api.Queries;
using BuildingBlocks.MongoDB;

namespace Bcc.Tenants.Api.Configuration;

public static class DependencyInjector
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<ITenantsQueries, TenantsQueries>();
        services.AddMongoDb(configuration);
        return services;
    }
}