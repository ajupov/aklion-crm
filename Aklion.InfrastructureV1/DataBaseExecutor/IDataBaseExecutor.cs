using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.InfrastructureV1.DataBaseExecutor
{
    public interface IDataBaseExecutor
    {
        Task Execute(string query, object parameters = null);

        Task<TModel> SelectOne<TModel>(string query, object parameters = null);

        Task<List<TModel>> SelectList<TModel>(string query, object parameters = null);
    }
}