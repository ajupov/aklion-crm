using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderSource;
using Aklion.Infrastructure.Mapper;
using DomainOrderSourceModel = Aklion.Crm.Domain.OrderSource.OrderSourceModel;
using DomainOrderSourceParameterModel = Aklion.Crm.Domain.OrderSource.OrderSourceParameterModel;
using DomainOrderSourceSelectParameterModel = Aklion.Crm.Domain.OrderSource.OrderSourceSelectParameterModel;

namespace Aklion.Crm.Mappers.User.OrderSource
{
    public static class OrderSourceMapper
    {
        public static PagingModel<OrderSourceModel> MapNew(this (int TotalCount, List<DomainOrderSourceModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderSourceModel>(tuple.List.MapListNew<OrderSourceModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderSourceModel MapNew(this OrderSourceModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderSourceModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderSourceModel MapFrom(this DomainOrderSourceModel domainModel, OrderSourceModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderSourceParameterModel MapNew(this OrderSourceParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderSourceParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderSourceSelectParameterModel MapNew(this int storeId)
        {
            return new DomainOrderSourceSelectParameterModel
            {
                StoreId = storeId
            };
        }

        public static Dictionary<string, int> MapNew(this Dictionary<string, int> models)
        {
            models.TryAdd(string.Empty, 0);

            return models.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}