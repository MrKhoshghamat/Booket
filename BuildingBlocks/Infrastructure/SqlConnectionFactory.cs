using System.Data;
using Microsoft.Data.SqlClient;
using Booket.BuildingBlocks.Application.Data;

namespace Booket.BuildingBlocks.Infrastructure
{
    public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable
    {
        private IDbConnection _connection;

        public IDbConnection GetOpenConnection()
        {
            if (_connection is { State: ConnectionState.Open })
            {
                return _connection;
            }

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            return _connection;
        }

        public IDbConnection CreateNewConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        public string GetConnectionString()
        {
            return connectionString;
        }

        public void Dispose()
        {
            if (_connection is { State: ConnectionState.Open })
            {
                _connection.Dispose();
            }
        }
    }
}