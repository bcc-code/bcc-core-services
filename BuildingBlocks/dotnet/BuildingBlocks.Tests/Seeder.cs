using Dapper;
using Microsoft.Data.SqlClient;

namespace BuildingBlocks.Tests
{
    public static class Seeder
    {
        public static void AddEntity(SqlConnection connection, object entity, string collection)
        {

            var allowedTypes = new[]
                {typeof(DateTime), typeof(int), typeof(string), typeof(decimal), typeof(MemberId), typeof(Guid)};
            
            var properties = entity.GetType().GetProperties()
                .Where(x => allowedTypes.Contains(x.PropertyType)).ToList();

            var columnNames = properties.Select(x => x.Name).Aggregate((x, y) => $"{x},{y}");
            var columnValues = properties.Select(x => $"@{x.Name}").Aggregate((x, y) => $"{x},{y}");
            if (string.IsNullOrEmpty(collection) || string.IsNullOrEmpty(columnNames))
            {
                throw new ArgumentNullException();
            }
            
            var query = @$"BEGIN
               IF NOT EXISTS (SELECT * FROM {collection} 
                               WHERE Id = @id)
               BEGIN
                  INSERT INTO {collection} ({columnNames}) VALUES ({columnValues})
               END
            END";

            connection.Execute(query, entity);
        }
    }   
}