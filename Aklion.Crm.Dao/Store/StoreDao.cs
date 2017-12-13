using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Store;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.Store
{
    public class StoreDao : IStoreDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public StoreDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<StoreModel>> GetPagedList(StoreParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<StoreModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern)
        {
            return _dataBaseExecutor.SelectListAsync<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern});
        }

        public Task<StoreModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOneAsync<StoreModel>(Queries.Get, new {id});
        }

        public Task<int> Create(StoreModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }

        public Task Update(StoreModel model)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Delete, new {id});
        }
    }
}