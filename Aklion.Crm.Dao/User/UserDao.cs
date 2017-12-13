using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.User;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.User
{
    public class UserDao : IUserDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<UserModel>> GetPagedList(UserParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<UserModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<List<AutocompleteModel>> GetForAutocompleteByLoginPattern(string pattern)
        {
            return _dataBaseExecutor.SelectListAsync<AutocompleteModel>(Queries.GetForAutocompleteByLoginPattern,
                new {pattern});
        }

        public Task<UserModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOneAsync<UserModel>(Queries.Get, new {id});
        }

        public Task<UserModel> GetByLogin(string login)
        {
            return _dataBaseExecutor.SelectOneAsync<UserModel>(Queries.GetByLogin, new {login});
        }

        public Task<UserModel> GetByEmail(string email)
        {
            return _dataBaseExecutor.SelectOneAsync<UserModel>(Queries.GetByEmail, new {email});
        }

        public Task<bool> IsExistByLogin(string login)
        {
            return _dataBaseExecutor.SelectOneAsync<bool>(Queries.IsExistByLogin, new {login});
        }

        public Task<bool> IsExistByEmail(string email)
        {
            return _dataBaseExecutor.SelectOneAsync<bool>(Queries.IsExistByEmail, new {email});
        }

        public Task<bool> IsExistByPhone(string phone)
        {
            return _dataBaseExecutor.SelectOneAsync<bool>(Queries.IsExistByPhone, new {phone});
        }

        public Task<int> Create(UserModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }

        public Task Update(UserModel model)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Delete, new {id});
        }
    }
}