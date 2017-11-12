using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Store;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

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
            return _dataBaseExecutor.SelectList<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern});
        }

        public Task<StoreModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<StoreModel>(Queries.Get, new {id});
        }

        public Task<int> Create(StoreModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(StoreModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}