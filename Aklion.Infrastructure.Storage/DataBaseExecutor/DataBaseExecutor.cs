﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Infrastructure.ConnectionFactory;
using Aklion.Infrastructure.Reader;
using Dapper;

namespace Aklion.Infrastructure.DataBaseExecutor
{
    public sealed class DataBaseExecutor : IDataBaseExecutor
    {
        private readonly IConnectionFactory _connectionFactory;

        public DataBaseExecutor(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task ExecuteAsync(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                await connection.ExecuteAsync(query, parameters).ConfigureAwait(false);
            }
        }

        public async Task<TModel> SelectOneAsync<TModel>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<TModel>(query, parameters).ConfigureAwait(false);
            }
        }

        public async Task<List<TModel>> SelectListAsync<TModel>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                return (await connection.QueryAsync<TModel>(query, parameters).ConfigureAwait(false)).ToList();
            }
        }

        public async Task<T> SelectMultipleAsync<T>(string query, Func<IReader, Task<T>> reader, object parameters = null)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var gridReader = await connection.QueryMultipleAsync(query, parameters).ConfigureAwait(false);

                return await reader(new Reader.Reader(gridReader)).ConfigureAwait(false);
            }
        }
    }
}