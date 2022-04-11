using Bcc.Tenants.Api.Queries;
using Microsoft.Extensions.FileProviders;

namespace Bcc.Tenants.Api.Configuration;

public static class DependencyInjector
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
        ConfigurationManager configuration, IWebHostEnvironment webHostEnvironment)
    {
        services.AddSingleton<ITenantsQueries, TenantsQueries>();
        services.AddSingleton<TenantsDataService>();
        
        var physicalProvider = webHostEnvironment.ContentRootFileProvider;
        services.AddSingleton<IFileProvider>(physicalProvider);
        return services;
    }
}