using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.UserPermission
{
    public class UserPermissionDao : IUserPermissionDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserPermissionDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<UserPermissionModel>> GetPagedList(UserPermissionParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<UserPermissionModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<UserPermissionModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<UserPermissionModel>(Queries.Get, new {id});
        }

        public Task<int> Create(UserPermissionModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(UserPermissionModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}