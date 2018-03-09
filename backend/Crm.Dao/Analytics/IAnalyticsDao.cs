using System.Threading.Tasks;
using Crm.Domain.Analytics;

namespace Crm.Dao.Analytics
{
    public interface IAnalyticsDao
    {
        Task<GeneralCountsModel> GetGeneralCountsAsync(int userId);
    }
}