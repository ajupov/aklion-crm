using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Infrastructure.Storage.DataBaseExecutor
{
    public interface IDataBaseExecutor
    {
        Task Execute(string query, object parameters = null);

        Task<T> SelectOne<T>(string query, object parameters = null);

        Task<List<T>> SelectList<T>(string query, object parameters = null);

        Task<KeyValuePair<int, List<T>>> SelectListWithTotalCount<T>(string query, object parameters = null);
    }
}