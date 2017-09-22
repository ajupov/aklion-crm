using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Infrastructure.Storage.Repository
{
    public interface IRepository
    {
        Task<TModel> Get<TModel>(int id);

        Task<(int, List<TModel>)> Get<TModel>(object parameters);

        Task<int> Create<TModel>(TModel model);

        Task Update<TModel>(TModel model);

        Task Delete<TModel>(int id);
    }
}