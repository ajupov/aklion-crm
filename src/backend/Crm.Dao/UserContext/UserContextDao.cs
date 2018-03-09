using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.Store;
using Crm.Domain.User;
using Crm.Domain.UserContext;
using Crm.Domain.UserPermission;
using Infrastructure.DataBaseExecutor;

namespace Crm.Dao.UserContext
{
    public class UserContextDao : IUserContextDao
    {
        private readonly IDataBaseExecutor _executor;

        public UserContextDao(IDataBaseExecutor executor)
        {
            _executor = executor;
        }

        public Task<UserContextModel> GetAsync(int userId, int storeId)
        {
            return _executor.SelectMultipleAsync(Queries.Get, async r => new UserContextModel
            {
                CurrentUser = await r.SelectOneAsync<UserModel>().ConfigureAwait(false),
                CurrentStore = await r.SelectOneAsync<StoreModel>().ConfigureAwait(false),
                CurrentStorePermissions = await r.SelectListAsync<UserPermissionModel>().ConfigureAwait(false)
            }, new {userId, storeId});
        }

        public Task<List<UserAvialableStoreModel>> GetAvialableStoresAsync(int userId)
        {
            return _executor.SelectListAsync<UserAvialableStoreModel>(Queries.GetAvialableStores, new {userId});
        }
    }
}