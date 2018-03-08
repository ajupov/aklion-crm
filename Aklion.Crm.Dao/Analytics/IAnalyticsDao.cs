using System.Threading.Tasks;
using Aklion.Crm.Domain.Analytics;

namespace Aklion.Crm.Dao.Analytics
{
    public interface IAnalyticsDao
    {
        Task<GeneralCountsModel> GetGeneralCountsAsync(int userId);
    }
}