using System.Data;
using Microsoft.Data.SqlClient;

namespace BuildingBlocks.Sql
{
    public interface ISqlConnectionService
    {
        Task<SqlConnection> GetAsync();
    }
    public class SqlConnectionService : ISqlConnectionService
    {
        private readonly string _connectionString;
        private SqlConnection _sqlConnection = null!;

        public SqlConnectionService(string connectionString)
        {
            _connectionString = string.IsNullOrEmpty(connectionString) == false ? connectionString : throw new ArgumentNullException(connectionString);
        }
        
        public async Task<SqlConnection> GetAsync()
        {
            if (_sqlConnection is not {State: ConnectionState.Closed})
            {
                return _sqlConnection;
            }
            
            if (string.IsNullOrEmpty(_connectionString) == false)
            {
                _sqlConnection = new SqlConnection(_connectionString);
                await _sqlConnection.OpenAsync();
            }
            
            if (_sqlConnection == null)
            {
                throw new ArgumentNullException(nameof(_sqlConnection), "SqlConnection not established");
            }

            return _sqlConnection;
        }
        //
        // public void Dispose()
        // {
        //     _sqlConnection?.Dispose();
        // }
    }
}