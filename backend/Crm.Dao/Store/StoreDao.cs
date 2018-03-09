using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.Store;
using Infrastructure.Dao;

namespace Crm.Dao.Store
{
    public class StoreDao : IStoreDao
    {
        private readonly IDao _dao;

        public StoreDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<StoreModel> List)> GetPagedListAsync(StoreParameterModel parameter)
        {
            return _dao.GetPagedListAsync<StoreModel, StoreParameterModel>(parameter);
        }

        public Task<(int TotalCount, List<StoreByUserModel> List)> GetPagedListAsync(StoreByUserParameterModel parameter)
        {
            return _dao.GetPagedListAsync<StoreByUserModel, StoreByUserParameterModel>(parameter);
        }

        public Task<List<StoreByUserModel>> GetListAsync(StoreByUserParameterModel parameter)
        {
            return _dao.GetListAsync<StoreByUserModel, StoreByUserParameterModel>(parameter, true);
        }

        public Task<Dictionary<string, int>> GetAutocompleteAsync(StoreAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<StoreModel, StoreAutocompleteParameterModel>(parameter);
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