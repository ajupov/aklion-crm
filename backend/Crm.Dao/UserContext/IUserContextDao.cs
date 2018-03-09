using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.UserContext;

namespace Crm.Dao.UserContext
{
    public interface IUserContextDao
    {
        Task<UserContextModel> GetAsync(int userId, int storeId);

        Task<List<UserAvialableStoreModel>> GetAvialableStoresAsync(int userId);
    }
}