using System.Threading.Tasks;
using Aklion.Crm.Domain.UserContext;

namespace Aklion.Crm.Dao.UserContext
{
    public interface IUserContextDao
    {
        Task<UserContextModel> GetAsync(string login, int selectedStoreId);
    }
}