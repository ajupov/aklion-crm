using Aklion.Crm.Models.Analytics;
using Aklion.Infrastructure.Mapper;
using DomainGeneralCountsModel = Aklion.Crm.Domain.Analytics.GeneralCountsModel;

namespace Aklion.Crm.Mappers.Analytics
{
    public static class AnalyticsMapper
    {
        public static GeneralCountsModel MapNew(this DomainGeneralCountsModel model)
        {
            return model.MapNew<GeneralCountsModel>();
        }
    }
}