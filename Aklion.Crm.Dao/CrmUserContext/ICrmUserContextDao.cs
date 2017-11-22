using System.Threading.Tasks;
using Aklion.Crm.Domain.CrmUserContext;

namespace Aklion.Crm.Dao.CrmUserContext
{
    public interface ICrmUserContextDao
    {
        Task<CrmUserContextModel> Get(string login, int selectedStoreId);
    }
}