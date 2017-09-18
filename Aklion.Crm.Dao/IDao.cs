using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Crm.Dao
{
    public interface IDao
    {
        Task<TModel> Get<TModel>(int id);

        Task<List<TModel>> GetList<TModel>(int page, int size);

        Task<int> Create<TModel>(TModel model);

        Task Update<TModel>(TModel model);

        Task Delete<TModel>(int id);
    }
}