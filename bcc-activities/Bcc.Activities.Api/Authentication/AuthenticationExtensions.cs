using BuildingBlocks.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bcc.Activities.Api.Authentication;

public static class AuthenticationExtensions
{
    public static void AddBccAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetSection("Auth").Get<AuthOptions>();
        if (authOptions == null)
        {
            throw new ArgumentNullException(nameof(AuthOptions));
        }

        if (string.IsNullOrEmpty(authOptions.Audience))
        {
            throw new ArgumentNullException(nameof(authOptions.Audience));
        }

        if (string.IsNullOrEmpty(authOptions.Authority))
        {
            throw new ArgumentNullException(nameof(authOptions.Authority));
        }

        if (authOptions.Authority.StartsWith("http"))
        {
            throw new ArgumentException("Authority shouldn't contain https://");
        }

        services.AddSingleton(s => authOptions);

        var openApiOptions = configuration.GetOpenApiOptions();

        if (openApiOptions.AuthenticationType is WebAuthenticationType.Test or WebAuthenticationType.LoadTest)
        {
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = TestAuthenticationScheme.AuthenticationScheme;
                    o.DefaultChallengeScheme = TestAuthenticationScheme.AuthenticationScheme;
                })
                .AddScheme<AuthenticationSchemeOptions, BuildingBlocks.Api.Authentication.TestAuthenticationHandler>(
                    TestAuthenticationScheme.AuthenticationScheme, o => { });
        }

        else
        {
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{authOptions.Authority}";
                    options.Audience = authOptions.Audience;
                });
            ;
        }
    }
}