using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Aklion.Infrastructure.Storage.ConnectionFactory
{
    public sealed class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}