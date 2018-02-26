using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Store;

namespace Aklion.Crm.Dao.Store
{
    public interface IStoreDao
    {
        Task<(int TotalCount, List<StoreModel> List)> GetPagedListAsync(StoreParameterModel parameter);

        Task<(int TotalCount, List<StoreByUserModel> List)> GetPagedListAsync(StoreByUserParameterModel parameter);

        Task<List<StoreByUserModel>> GetListAsync(StoreByUserParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(StoreAutocompleteParameterModel parameter);

        Task<StoreModel> GetAsync(int id);

        Task<int> CreateAsync(StoreModel model);

        Task UpdateAsync(StoreModel model);

        Task DeleteAsync(int id);
    }
}