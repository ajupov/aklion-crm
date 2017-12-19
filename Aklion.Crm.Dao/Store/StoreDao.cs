using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Store;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.Store
{
    public class StoreDao : IStoreDao
    {
        private readonly IDao _dao;

        public StoreDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<StoreModel>>> GetPagedListAsync(StoreParameterModel parameter)
        {
            return _dao.GetPagedListAsync<StoreModel, StoreParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(StoreAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync(parameter);
        }

        public Task<StoreModel> GetAsync(int id)
        {
            return _dao.GetAsync<StoreModel>(id);
        }

        public Task<int> CreateAsync(StoreModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(StoreModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<StoreModel>(id);
        }
    }
}