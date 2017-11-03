using System.Threading.Tasks;
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

        public Task<UserModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<UserModel>(Queries.Get, new {id});
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