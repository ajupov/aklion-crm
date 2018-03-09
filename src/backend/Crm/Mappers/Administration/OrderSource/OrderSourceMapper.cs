using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.Administration.OrderSource;
using Infrastructure.Mapper;
using DomainOrderSourceModel = Crm.Domain.OrderSource.OrderSourceModel;
using DomainOrderSourceParameterModel = Crm.Domain.OrderSource.OrderSourceParameterModel;
using DomainOrderSourceSelectParameterModel = Crm.Domain.OrderSource.OrderSourceSelectParameterModel;

namespace Crm.Mappers.Administration.OrderSource
{
    public static class OrderSourceMapper
    {
        public static PagingModel<OrderSourceModel> MapNew(this (int TotalCount, List<DomainOrderSourceModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderSourceModel>(tuple.List.MapListNew<OrderSourceModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderSourceModel MapNew(this OrderSourceModel model)
        {
            return model.MapNew<DomainOrderSourceModel>();
        }

        public static DomainOrderSourceModel MapFrom(this DomainOrderSourceModel domainModel, OrderSourceModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderSourceParameterModel MapNew(this OrderSourceParameterModel model)
        {
            return model.MapParameterNew<DomainOrderSourceParameterModel>();
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