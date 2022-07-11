using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Api.Logging;

public static class LoggingExtensions
{
    public static void AddBccLogging(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        services.AddTransient<ITelemetryInitializer, AppInsightsInitializer>();
        services.AddTransient<ITelemetryParameters, TelemetryParameters>();
        services.AddTransient<ILogService, LogService>();
        
        services.AddApplicationInsightsTelemetry();

    }
}