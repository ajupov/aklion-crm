using System.Data;

namespace Aklion.Infrastructure.Storage.ConnectionFactory
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}