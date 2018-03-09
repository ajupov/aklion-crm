using Crm.Models.Analytics;
using Infrastructure.Mapper;
using DomainGeneralCountsModel = Crm.Domain.Analytics.GeneralCountsModel;

namespace Crm.Mappers.Analytics
{
    public static class AnalyticsMapper
    {
        public static GeneralCountsModel MapNew(this DomainGeneralCountsModel model)
        {
            return model.MapNew<GeneralCountsModel>();
        }
    }
}