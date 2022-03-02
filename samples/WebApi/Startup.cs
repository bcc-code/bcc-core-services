using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.OpenApi;
using BuildingBlocks.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            OpenApiOptions = new OpenApiOptions
            {
                ApiTitle = "WebApi v1",
                ApiVersion = "/swagger/v1/swagger.json",
                AuthenticationType = WebAuthenticationType.Auth0
            };
        }

        public OpenApiOptions OpenApiOptions { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SqlDataContext");
            
            services.InitializeDapper(connectionString);
            services.AddControllers();
            services.AddBccSwagger(OpenApiOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseBccSwagger(OpenApiOptions);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
