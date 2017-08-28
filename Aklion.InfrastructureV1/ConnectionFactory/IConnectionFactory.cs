using System.Data;

namespace Aklion.InfrastructureV1.ConnectionFactory
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}