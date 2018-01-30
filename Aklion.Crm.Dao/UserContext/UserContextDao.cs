using System.Collections.Generic;
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

        public Task<UserContextModel> GetAsync(int userId, int storeId)
        {
            return _dataBaseExecutor.SelectMultipleAsync(Queries.Get, async r => new UserContextModel
            {
                CurrentUser = await r.SelectOneAsync<UserModel>().ConfigureAwait(false),
                CurrentStore = await r.SelectOneAsync<StoreModel>().ConfigureAwait(false),
                CurrentStorePermissions = await r.SelectListAsync<UserPermissionModel>().ConfigureAwait(false)
            }, new {userId, storeId});
        }

        public Task<List<UserAvialableStoreModel>> GetAvialableStoresAsync(int userId)
        {
            return _dataBaseExecutor.SelectListAsync<UserAvialableStoreModel>(Queries.GetAvialableStores, new {userId});
        }
    }
}