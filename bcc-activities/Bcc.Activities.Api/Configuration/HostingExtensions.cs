using System.Reflection;

namespace Bcc.Activities.Api.Configuration;

public static class HostingExtensions
{
    public static IHostBuilder AddCoreAppSettings(this IHostBuilder host) =>
        host.ConfigureAppConfiguration((hostingContext, builder) =>
        {
            IHostEnvironment env = hostingContext.HostingEnvironment;
                
            builder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.tests.json", true);
                
            if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
            {
                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                builder.AddUserSecrets(appAssembly, optional: true);
            }
        });
}