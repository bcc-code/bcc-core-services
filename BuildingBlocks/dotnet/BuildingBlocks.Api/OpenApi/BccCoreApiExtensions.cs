using System.Reflection;
using System.Reflection.Emit;
using BuildingBlocks.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

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

            if (authOptions.Authority.StartsWith("http"))
            {
                throw new ArgumentException("Authority shouldn't contain https://");
            }

            services.AddSingleton(s => authOptions);

            var openApiOptions = ValidateOpenApiOptions(configuration);

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
                    .AddJwtBearer(options =>
                    {
                        options.Authority = $"https://{authOptions.Authority}";
                        options.Audience = authOptions.Audience;
                    })
                    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
                        ApiKeyAuthenticationScheme.AuthenticationScheme, o => { });
            }
        }

        public static void AddBccSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = ValidateOpenApiOptions(configuration);
            services.AddSingleton(s => options);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Version,
                    new OpenApiInfo {Title = options.Title, Version = options.Version});
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityDefinition("ClientCredentials", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = null,
                    In = ParameterLocation.Header,
                    OpenIdConnectUrl = new Uri("/docs/token"),
                    Flows = new()
                    {
                        ClientCredentials = new()
                        {
                            TokenUrl = new Uri("/docs/token", UriKind.Relative),
                            Scopes = options.Scopes
                        },
                    }
                });

                // JWT Authentication
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
                
                // Client Credentials Flow 
                // @see https://auth0.com/docs/get-started/authentication-and-authorization-flow/client-credentials-flow

                
                // including comments from classes/contracts available for API 
                // @see https://github.com/domaindrivendev/Swashbuckle.AspNetCore#include-descriptions-from-xml-comments
                var assembly = Assembly.GetEntryAssembly();
                try
                {
                    var filePath = Path.Combine(System.AppContext.BaseDirectory, $"{assembly?.GetName().Name}.xml");
                    c.IncludeXmlComments(filePath);
                }
                catch (Exception ex)
                {
                    //File won't be found for integration tests where Entry Assembly is ReSharperTestRunner
                }
            });
        }

        public static void UseBccSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = ValidateOpenApiOptions(configuration);
            
            app.UseSwagger(o =>
            {
                o.SerializeAsV2 = true;
            });
            app.UseSwaggerUI();
            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.Title} {options.Version}");
            // });
        }

        public static OpenApiOptions ValidateOpenApiOptions(IConfiguration configuration)
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

            return options;
        }
    }
}