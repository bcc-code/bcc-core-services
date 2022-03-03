using System.Reflection;
using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.OpenApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Api.Extensions
{
    public static class HostingExtensions
    {
        public static IHostBuilder AddCoreAppSettings(this IHostBuilder host) =>
            host.ConfigureAppConfiguration((hostingContext, builder) =>
            {
                IHostEnvironment env = hostingContext.HostingEnvironment;
                
                builder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile("appsettings.core.json")
                    .AddJsonFile("appsettings.tests.json", true);
                
                if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        builder.AddUserSecrets(appAssembly, optional: true);
                    }
                }
            });
    }
}