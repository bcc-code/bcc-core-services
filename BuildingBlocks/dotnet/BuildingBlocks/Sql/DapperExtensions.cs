using Dapper;

namespace BuildingBlocks.Sql
{

    public static class DapperExtensions
    {
        public static void AddConverters()
        {
            SqlMapper.AddTypeHandler(new MemberIdConverter());
            SqlMapper.AddTypeHandler(new DateTimeConverter());

        }

    }
}