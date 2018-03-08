using System.Threading.Tasks;
using Aklion.Crm.Domain.Analytics;
using Aklion.Infrastructure.DataBaseExecutor;

namespace Aklion.Crm.Dao.Analytics
{
    public class AnalyticsDao : IAnalyticsDao
    {
        private readonly IDataBaseExecutor _executor;

        public AnalyticsDao(IDataBaseExecutor executor)
        {
            _executor = executor;
        }

        public Task<GeneralCountsModel> GetGeneralCountsAsync(int userId)
        {
            return _executor.SelectOneAsync<GeneralCountsModel>(Queries.GetGeneralCounts, new {userId});
        }
    }
}