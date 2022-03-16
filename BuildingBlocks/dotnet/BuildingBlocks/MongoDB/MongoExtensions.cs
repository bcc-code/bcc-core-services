using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.MongoDB
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoService, MongoService>();
            
            var options = configuration.GetSection("MongoDb").Get<MongoDbOptions>();
            services.AddSingleton(_ => options);

            return services;
        }
    }
}