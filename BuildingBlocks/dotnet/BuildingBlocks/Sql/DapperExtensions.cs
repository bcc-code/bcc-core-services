using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Sql
{

    public static class DapperExtensions
    {
        public static void InitializeDapper(this IServiceCollection services, string connectionString)
        {
            SqlMapper.AddTypeHandler(new MemberIdConverter());
            SqlMapper.AddTypeHandler(new DateTimeConverter());
            
            services.AddScoped<ISqlConnectionService, SqlConnectionService>(x => new SqlConnectionService(connectionString));
        }
    }
}