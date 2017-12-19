using System.Threading.Tasks;
using Aklion.Crm.Domain.Store;
using Aklion.Crm.Domain.User;
using Aklion.Crm.Domain.UserContext;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Infrastructure.DataBaseExecutor;

namespace Aklion.Crm.Dao.UserContext
{
    public class UserContextDao : IUserContextDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserContextDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<UserContextModel> GetAsync(string login, int selectedStoreId)
        {
            return _dataBaseExecutor.SelectMultipleAsync(Queries.Get, async r => new UserContextModel
            {
                CurrentUser = await r.SelectOneAsync<UserModel>().ConfigureAwait(false),
                CurrentStore = await r.SelectOneAsync<StoreModel>().ConfigureAwait(false),
                CurrentStorePermissions = await r.SelectListAsync<UserPermissionModel>().ConfigureAwait(false),
                Stores = await r.SelectListAsync<StoreModel>().ConfigureAwait(false)
            }, new {login, selectedStoreId});
        }
    }
}