using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Middlewares;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUser, TestUser>();
            
            services.ConfigureBlocks(Configuration, Environment);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureBlocks(Configuration, Environment);

            app.UseAuthentication();
            app.UseMiddleware<AddingCustomClaimsMiddleware>();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
