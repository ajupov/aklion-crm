using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Aklion.Infrastructure.Storage.ConnectionFactory
{
    public sealed class MsSqlServerConnectionFactory : IConnectionFactory
    {
        private readonly string _msSqlServerConnectionString;

        public MsSqlServerConnectionFactory(IConfiguration configuration)
        {
            _msSqlServerConnectionString = configuration.GetConnectionString("MsSqlServerConnection");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_msSqlServerConnectionString);
        }
    }
}