using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ConnectionFactory
{
    public sealed class ConnectionFactory : IConnectionFactory
    {
        private readonly string _string;

        public ConnectionFactory(IConfiguration configuration)
        {
            _string = configuration.GetConnectionString("ConnectionString");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_string);
        }
    }
}