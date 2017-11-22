using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Readers;

namespace Aklion.Infrastructure.Storage.DataBaseExecutor
{
    public interface IDataBaseExecutor
    {
        Task Execute(string query, object parameters = null);

        Task<T> SelectOne<T>(string query, object parameters = null);

        Task<List<T>> SelectList<T>(string query, object parameters = null);

        Task<Paging<T>> SelectListWithTotalCount<T>(string query, object parameters = null);

        Task SelectMultiple(string query, Func<IReader, Task> reader, object parameters = null);

        Task<T> SelectMultiple<T>(string query, Func<IReader, Task<T>> reader, object parameters = null);
    }
}