using Bcc.Activities.Api.Authentication;
using Bcc.Activities.Api.Services;
using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.Extensions;
using BuildingBlocks.MongoDB;

namespace Bcc.Activities.Api.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.ConfigureBlocks(configuration, environment);
        services.AddBccAuthentication(configuration);

        services.AddTransient<IUser, ApplicationUser>();

        services.AddScoped<IActivityService, ActivityService>();
        
        services.AddAutoMapperConfiguration();

        services.AddMongoDb(configuration);
        
        return services;
    }
}