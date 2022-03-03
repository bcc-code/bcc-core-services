using BuildingBlocks.Api.Authentication;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BuildingBlocks.Api.OpenApi
{
    public static class BccCoreApiExtensions
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
            services.AddSingleton(s => authOptions);
            
            var openApiOptions = configuration.GetSection("Api").Get<OpenApiOptions>();
            
            if (openApiOptions.AuthenticationType is WebAuthenticationType.Test or WebAuthenticationType.LoadTest)
            {
                services.AddAuthentication(o =>
                    {
                        o.DefaultAuthenticateScheme = TestAuthenticationScheme.AuthenticationScheme;
                        o.DefaultChallengeScheme = TestAuthenticationScheme.AuthenticationScheme;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                        TestAuthenticationScheme.AuthenticationScheme, o => { });
            }

            else
            {
                services.AddAuthentication(o =>
                    {
                        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(o =>
                    {
                        o.Authority = $"https://{authOptions.Authority}";
                        o.Audience = authOptions.Audience;
                    });
            }
        }

        public static void AddBccSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("Api").Get<OpenApiOptions>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(OpenApiOptions));
            }
            if (string.IsNullOrEmpty(options.Title))
            {
                throw new ArgumentNullException(nameof(options.Title));
            }
            if (string.IsNullOrEmpty(options.Version))
            {
                throw new ArgumentNullException(nameof(options.Version));
            }
            if (string.IsNullOrEmpty(options.AuthenticationType))
            {
                throw new ArgumentNullException(nameof(options.AuthenticationType));
            }
            services.AddSingleton(s => options);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Version,
                    new OpenApiInfo {Title = options.Title, Version = options.Version});

                switch (options.AuthenticationType)
                {
                    case WebAuthenticationType.Test:
                    {
                        c.OperationFilter<TestHeaderFilter>();
                        break;
                    }
                    
                    default:
                    {
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description =
                                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        });
                        break;
                    }
                }
            });
        }

        public static void AddBccTelemetry(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationInsightsTelemetry(options => options.ConnectionString = connectionString);
            services.AddSingleton<TelemetryClient>();
        }

        public static void UseBccSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = configuration.GetSection("Api").Get<OpenApiOptions>();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.Title} {options.Version}"); });
        }
    }
}