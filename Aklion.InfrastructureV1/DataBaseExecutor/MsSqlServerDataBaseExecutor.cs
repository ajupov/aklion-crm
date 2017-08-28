using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.InfrastructureV1.ConnectionFactory;
using Dapper;

namespace Aklion.InfrastructureV1.DataBaseExecutor
{
    public sealed class MsSqlServerDataBaseExecutor : IDataBaseExecutor
    {
        private readonly IConnectionFactory _connectionFactory;

        public MsSqlServerDataBaseExecutor(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                await connection.ExecuteAsync(query, parameters).ConfigureAwait(false);
            }
        }

        public async Task<TModel> SelectOne<TModel>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<TModel>(query, parameters).ConfigureAwait(false);
            }
        }

        public async Task<List<TModel>> SelectList<TModel>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                return (await connection.QueryAsync<TModel>(query, parameters).ConfigureAwait(false)).ToList();
            }
        }
    }
}