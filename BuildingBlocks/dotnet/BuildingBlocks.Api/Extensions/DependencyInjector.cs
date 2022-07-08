using BuildingBlocks.Api.Logging;
using BuildingBlocks.Api.Middlewares;
using BuildingBlocks.Api.OpenApi;
using BuildingBlocks.Sql;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Api.Extensions
{
    public static class DependencyInjector
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        public static void ConfigureBlocks(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddHealthChecks();
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            var connectionString = configuration.GetConnectionString("SqlDataContext");
            services.InitializeDapper(connectionString);
               
            if (environment.IsProduction() == false)
            {
                services.AddBccSwagger(configuration);
            }

            services.AddBccLogging();
            
            services.RegisterCors(configuration);
        }

        private static void RegisterCors(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration["origins"];
            if (string.IsNullOrEmpty(origins))
            {
                throw new ArgumentNullException(nameof(origins));
            }
            
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins(origins.Split(','))
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
            });
        }

        public static void ConfigureBlocks(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment environment)
        {
            app.UseCors(MyAllowSpecificOrigins);
            
            app.UseMiddleware<ExceptionMiddleware>(environment.IsProduction() == false);
            
            if (environment.IsProduction() == false)
            {
                app.UseDeveloperExceptionPage();
                
                app.UseBccSwagger(configuration);
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
        }
    }    
}
