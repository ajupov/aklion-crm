using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.User;
using Infrastructure.Dao;

namespace Crm.Dao.User
{
    public class UserDao : IUserDao
    {
        private readonly IDao _dao;

        public UserDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<UserModel> List)> GetPagedListAsync(UserParameterModel parameter)
        {
            return _dao.GetPagedListAsync<UserModel, UserParameterModel>(parameter);
        }

        public Task<(int TotalCount, List<UserByStoreModel> List)> GetPagedListAsync(UserByStoreParameterModel parameter)
        {
            return _dao.GetPagedListAsync<UserByStoreModel, UserByStoreParameterModel>(parameter, true);
        }

        public Task<Dictionary<string, int>> GetAutocompleteAsync(UserAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<UserModel, UserAutocompleteParameterModel>(parameter);
        }

        public Task<UserModel> GetAsync(int id)
        {
            return _dao.GetAsync<UserModel>(id);
        }

        public Task<int> CreateAsync(UserModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(UserModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<UserModel>(id);
        }

        public Task<UserModel> GetByLoginAsync(string login)
        {
            return _dao.GetAsync<UserModel, UserLoginParameterModel>(new UserLoginParameterModel{Login = login});
        }

        public Task<UserModel> GetByEmailAsync(string email)
        {
            return _dao.GetAsync<UserModel, UserEmailParameterModel>(new UserEmailParameterModel {Email = email});
        }

        public async Task<bool> IsExistByLoginAsync(string login)
        {
            var result = await _dao
                .GetAsync<UserModel, UserLoginParameterModel>(new UserLoginParameterModel {Login = login})
                .ConfigureAwait(false);

            return result != null;
        }

        public async Task<bool> IsExistByEmailAsync(string email)
        {
            var result = await _dao
                .GetAsync<UserModel, UserEmailParameterModel>(new UserEmailParameterModel {Email = email})
                .ConfigureAwait(false);

            return result != null;
        }

        public async Task<bool> IsExistByPhoneAsync(string phone)
        {
            var result = await _dao
                .GetAsync<UserModel, UserPhoneParameterModel>(new UserPhoneParameterModel {Phone = phone})
                .ConfigureAwait(false);

            return result != null;
        }
    }
}