using System.Data;

namespace Infrastructure.ConnectionFactory
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}