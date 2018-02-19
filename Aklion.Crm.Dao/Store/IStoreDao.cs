using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Store;

namespace Aklion.Crm.Dao.Store
{
    public interface IStoreDao
    {
        Task<Tuple<int, List<StoreModel>>> GetPagedListAsync(StoreParameterModel parameter);

        Task<Tuple<int, List<StoreByUserModel>>> GetPagedListAsync(StoreByUserParameterModel parameter);

        Task<List<StoreByUserModel>> GetListAsync(StoreByUserParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(StoreAutocompleteParameterModel parameter);

        Task<StoreModel> GetAsync(int id);

        Task<int> CreateAsync(StoreModel model);

        Task UpdateAsync(StoreModel model);

        Task DeleteAsync(int id);
    }
}