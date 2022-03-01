using BuildingBlocks.Api.Authentication;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BuildingBlocks.Api.OpenApi
{
    public static class BccCoreApiExtensions
    {
        public static void AddBccAuthentication(this IServiceCollection services, AuthOptions authOptions)
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

        public static void AddBccSwagger(this IServiceCollection services, OpenApiOptions options)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.ApiVersion,
                    new OpenApiInfo {Title = options.ApiTitle, Version = options.ApiVersion});

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
            });
        }

        public static void AddBccTelemetry(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationInsightsTelemetry(options => options.ConnectionString = connectionString);
            services.AddSingleton<TelemetryClient>();
        }

        public static void UseBccSwagger(this IApplicationBuilder app, OpenApiOptions options)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApiTitle} {options.ApiVersion}"); });
        }
    }
    
}