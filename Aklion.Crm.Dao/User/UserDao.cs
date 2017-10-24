using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Interfaces.User;
using Aklion.Crm.Domain.Models.User;
using Aklion.Infrastructure.Storage.DataBaseExecutor;

namespace Aklion.Crm.Dao.User
{
    public class UserDao : IUserDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<KeyValuePair<int, List<UserModel>>> GetList(object parameters)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<UserModel>(Queries.GetList, parameters);
        }

        public Task<UserModel> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Insert(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}