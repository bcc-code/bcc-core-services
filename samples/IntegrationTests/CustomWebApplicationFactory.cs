using System;
using System.Linq;
using System.Threading.Tasks;
using BuildingBlocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Respawn;
using WebApi.Services;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TEntryPoint>
        : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        // ReSharper disable once StaticMemberInGenericType
        private static Checkpoint _checkpoint = new();
        
        public Personas Personas { get; set; } = new Personas();
        public SqlConnection? SqlConnection { get; set; } = null!;
        private ISqlConnectionService SqlConnectionService { get; set; } = null!;
        public ILogger<CustomWebApplicationFactory<TEntryPoint>> Logger { get; set; } = null!;

        public IConfiguration Configuration { get; set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            builder.UseEnvironment(Environments.Development);

            builder.ConfigureLogging(o =>
            {
                o.AddDebug();
                o.AddConsole();
                o.AddFilter(level => level > LogLevel.Warning);
            });
            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                Logger = sp.GetRequiredService<ILogger<CustomWebApplicationFactory<TEntryPoint>>>();

                Configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = Configuration.GetConnectionString("IntegrationTestsContext");
                var sqlConnectionService = services.Single(d => d.ServiceType == typeof(ISqlConnectionService));
                services.Remove(sqlConnectionService);
                services.AddScoped<ISqlConnectionService>(x =>
                    new SqlConnectionService(connectionString));

                Logger.Log(LogLevel.Debug, "ConnectionString changed to: {ConnectionString}", connectionString);

                SqlConnectionService = services.BuildServiceProvider().GetRequiredService<ISqlConnectionService>();
                ServiceProvier = services.BuildServiceProvider();
            });
        }

        public ServiceProvider ServiceProvier { get; set; }

        // protected override async void Dispose(bool disposing)
        // {
        //     if (disposing)
        //     {
        //         var connectionString = Configuration.GetConnectionString("IntegrationTestsContext");
        //         if (string.IsNullOrEmpty(connectionString) == false)
        //         {
        //             await _checkpoint.Reset(connectionString);
        //         }
        //     }
        //
        //     base.Dispose(disposing);
        // }

        private bool _initialized;
        public async Task InitSeed()
        {
            if (_initialized)
            {
                return;  
            }
            
            SqlConnection = await SqlConnectionService.GetAsync();
            var connectionString = Configuration.GetConnectionString("IntegrationTestsContext");
            
            if (SqlConnection == null)
            {
                throw new ArgumentNullException(nameof(SqlConnection));
            }

            await _checkpoint.Reset(connectionString);

            _initialized = true;
        }


        public void SeedDatabase(Action<SqlConnection> callback)
        {   
            Logger.LogDebug("Seeding Database for {Database}", SqlConnection.Database);
           
            try
            { 
                callback(SqlConnection);
            }
            catch (Exception e)
            {
                Logger.LogError(e, nameof(SeedDatabase));
                throw;
            }
        }
    }
}