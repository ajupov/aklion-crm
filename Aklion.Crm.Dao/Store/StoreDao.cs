using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.Storage.DataBaseExecutor;

namespace Aklion.Crm.Dao.Store
{
    public class StoreDao : IStoreDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public StoreDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Models.Store> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<Models.Store>(Queries.Get, new {id});
        }

        public Task<List<Models.Store>> GetList(int page, int size)
        {
            return _dataBaseExecutor.SelectList<Models.Store>(Queries.GetList, new {page, size});
        }

        public Task<int> Create(Models.Store model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(Models.Store model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}