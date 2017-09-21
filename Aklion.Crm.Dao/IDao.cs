using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Crm.Dao
{
    public interface IDao
    {
        Task<TModel> Get<TModel>(int id);

        Task<int> GetCount<TModel>(object parameters);

        Task<List<TModel>> GetList<TModel>(object parameters);

        Task<int> Create<TModel>(TModel model);

        Task Update<TModel>(TModel model);

        Task Delete<TModel>(int id);
    }
}