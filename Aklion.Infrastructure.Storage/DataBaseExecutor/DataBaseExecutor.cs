using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Infrastructure.Storage.ConnectionFactory;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Readers;
using Dapper;

namespace Aklion.Infrastructure.Storage.DataBaseExecutor
{
    public sealed class DataBaseExecutor : IDataBaseExecutor
    {
        private readonly IConnectionFactory _connectionFactory;

        public DataBaseExecutor(IConnectionFactory connectionFactory)
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

        public async Task<Paging<T>> SelectListWithTotalCount<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var reader = await connection.QueryMultipleAsync(query, parameters).ConfigureAwait(false);

                return new Paging<T>
                {
                    TotalCount = await reader.ReadFirstOrDefaultAsync<int>().ConfigureAwait(false),
                    List = (await reader.ReadAsync<T>().ConfigureAwait(false)).ToList()
                };
            }
        }

        public async Task SelectMultiple(string query, Func<IReader, Task> reader, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var  gridReader = await connection.QueryMultipleAsync(query, parameters).ConfigureAwait(false);

                await reader(new Reader(gridReader)).ConfigureAwait(false);
            }
        }

        public async Task<T> SelectMultiple<T>(string query, Func<IReader, Task<T>> reader, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var gridReader = await connection.QueryMultipleAsync(query, parameters).ConfigureAwait(false);

                return await reader(new Reader(gridReader)).ConfigureAwait(false);
            }
        }
    }
}