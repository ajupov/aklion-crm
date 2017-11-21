using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.User;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

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
            return _dataBaseExecutor.SelectList<AutocompleteModel>(Queries.GetForAutocompleteByLoginPattern,
                new {pattern});
        }

        public Task<UserModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<UserModel>(Queries.Get, new {id});
        }

        public Task<UserModel> GetByLogin(string login)
        {
            return _dataBaseExecutor.SelectOne<UserModel>(Queries.GetByLogin, new {login});
        }

        public Task<UserModel> GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsExistByLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsExistByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsExistByPhone(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Create(UserModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(UserModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}