using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Infrastructure.Dao
{
    public interface IDao
    {
        Task<TModel> GetAsync<TModel>(int id);

        Task<TModel> GetAsync<TModel, TParameter>(TParameter parameter);

        Task<List<TModel>> GetListAsync<TModel, TParameter>(TParameter parameter);

        Task<Tuple<int, List<TModel>>> GetPagedListAsync<TModel, TParameter>(TParameter parameter);

        Task<Dictionary<string, int>> GetForAutoCompleteAsync<TParameter>(TParameter parameter);

        Task<int> CreateAsync<TModel>(TModel model);

        Task<bool> UpdateAsync<TModel>(TModel model);

        Task<bool> DeleteAsync<TModel>(int id);
    }
}