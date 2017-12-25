using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.Reader;

namespace Aklion.Infrastructure.DataBaseExecutor
{
    public interface IDataBaseExecutor
    {
        Task<bool> ExecuteAsync(string query, object parameters = null);

        Task<T> SelectOneAsync<T>(string query, object parameters = null);

        Task<List<T>> SelectListAsync<T>(string query, object parameters = null);

        Task<T> SelectMultipleAsync<T>(string query, Func<IReader, Task<T>> reader, object parameters = null);
    }
}