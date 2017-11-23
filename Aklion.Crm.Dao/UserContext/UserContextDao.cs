using System.Threading.Tasks;
using Aklion.Crm.Domain.Store;
using Aklion.Crm.Domain.User;
using Aklion.Crm.Domain.UserContext;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Infrastructure.Storage.DataBaseExecutor;

namespace Aklion.Crm.Dao.UserContext
{
    public class UserContextDao : IUserContextDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserContextDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<UserContextModel> Get(string login, int selectedStoreId)
        {
            return _dataBaseExecutor.SelectMultiple(Queries.Get, async r => new UserContextModel
            {
                CurrentUser = await r.SelectOne<UserModel>().ConfigureAwait(false),
                CurrentStore = await r.SelectOne<StoreModel>().ConfigureAwait(false),
                CurrentStorePermissions = await r.SelectList<UserPermissionModel>().ConfigureAwait(false),
                Stores = await r.SelectList<StoreModel>().ConfigureAwait(false)
            }, new {login, selectedStoreId});
        }
    }
}