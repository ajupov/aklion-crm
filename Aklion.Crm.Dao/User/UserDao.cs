using System.Threading.Tasks;
using Aklion.Crm.Domain.Interfaces.User;
using Aklion.Crm.Domain.Models.User;
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

        public Task<Paging<Domain.Models.User.User>> GetPagedList(UserParameter parameter)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<Domain.Models.User.User>(Queries.GetPagedList, parameter);
        }

        public Task<Domain.Models.User.User> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Insert(Domain.Models.User.User model)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Domain.Models.User.User model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}