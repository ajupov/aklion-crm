using System.Data;

namespace Aklion.Infrastructure.ConnectionFactory
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}