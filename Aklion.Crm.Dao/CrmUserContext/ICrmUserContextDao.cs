using System.Threading.Tasks;
using Aklion.Crm.Domain.UserContext;

namespace Aklion.Crm.Dao.CrmUserContext
{
    public interface ICrmUserContextDao
    {
        Task<UserContextModel> Get(string login, int selectedStoreId);
    }
}