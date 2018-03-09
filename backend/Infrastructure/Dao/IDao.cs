using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Dao
{
    public interface IDao
    {
        Task<TModel> GetAsync<TModel>(int id);

        Task<TModel> GetAsync<TModel, TParameter>(TParameter parameter);

        Task<List<TModel>> GetListAsync<TModel>(bool distinct = false);

        Task<List<TModel>> GetListAsync<TModel, TParameter>(TParameter parameter, bool distinct = false);

        Task<(int TotalCount, List<TModel> List)> GetPagedListAsync<TModel, TParameter>(TParameter parameter, bool distinct = false);

        Task<Dictionary<string, int>> GetForAutoCompleteAsync<TModel, TParameter>(TParameter parameter, bool distinct = false);

        Task<Dictionary<string, int>> GetForSelectAsync<TModel, TParameter>(TParameter parameter, bool distinct = false);

        Task<int> CreateAsync<TModel>(TModel model);

        Task CreateListAsync<TModel>(List<TModel> model);

        Task UpdateAsync<TModel>(TModel model);

        Task DeleteAsync<TModel>(int id);
    }
}