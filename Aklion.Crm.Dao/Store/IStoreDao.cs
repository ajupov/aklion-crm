using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Store;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.Store
{
    public interface IStoreDao
    {
        Task<Paging<StoreModel>> GetPagedList(StoreParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern);

        Task<StoreModel> Get(int id);

        Task<int> Create(StoreModel model);

        Task Update(StoreModel model);

        Task Delete(int id);
    }
}