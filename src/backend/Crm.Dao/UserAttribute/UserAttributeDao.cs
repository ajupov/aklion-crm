using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.UserAttribute;
using Infrastructure.Dao;

namespace Crm.Dao.UserAttribute
{
    public class UserAttributeDao : IUserAttributeDao
    {
        private readonly IDao _dao;

        public UserAttributeDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<UserAttributeModel> List)> GetPagedListAsync(UserAttributeParameterModel parameter)
        {
            return _dao.GetPagedListAsync<UserAttributeModel, UserAttributeParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetAutocompleteAsync(UserAttributeAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<UserAttributeModel, UserAttributeAutocompleteParameterModel>(parameter);
        }

        public Task<UserAttributeModel> GetAsync(int id)
        {
            return _dao.GetAsync<UserAttributeModel>(id);
        }

        public Task<int> CreateAsync(UserAttributeModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(UserAttributeModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<UserAttributeModel>(id);
        }
    }
}