using Bcc.Activities.Api.Services;
using Bcc.Activities.Tests.Activities;
using BuildingBlocks.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Respawn;
using Respawn.Graph;
using TestAuthenticationHandler = Bcc.Activities.Api.Authentication.TestAuthenticationHandler;

namespace Bcc.Activities.Tests;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    private static Checkpoint checkpoint = new Checkpoint
    {
        TablesToIgnore = new Table[]
        {
            "__EFMigrationsHistory",
            "_SchemaClubs",
            "_SchemaContributions",
            "_SchemaVersions"
        }
    };

    public ILogger<CustomWebApplicationFactory<TStartup>> Logger { get; set; } = null!;

    public ServiceProvider ServiceProviders { get; set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(".");
        builder.UseEnvironment(Environments.Development);

        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestAuthenticationScheme.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                    TestAuthenticationScheme.AuthenticationScheme, _ => { });
        });

        builder.ConfigureLogging(o =>
        {
            o.AddDebug();
            o.AddConsole();
            o.AddFilter(level => level > LogLevel.Warning);
        });
        builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();
            Logger = sp.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
            
            var activityService = services.Single(d => d.ServiceType == typeof(IActivityService));
            services.Remove(activityService);
            services.AddTransient<IActivityService, FakeActivityService>();

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    TestAuthenticationScheme.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            ServiceProviders = services.BuildServiceProvider();
        });
    }


    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    private bool _initialized;
    public Task InitSeed()
    {
        if (_initialized)
        {
            return Task.CompletedTask;
        }

        _initialized = true;
        return Task.CompletedTask;
    }
}