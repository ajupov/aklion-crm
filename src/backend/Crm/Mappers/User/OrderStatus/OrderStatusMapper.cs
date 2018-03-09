using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.User.OrderStatus;
using Infrastructure.Mapper;
using DomainOrderStatusModel = Crm.Domain.OrderStatus.OrderStatusModel;
using DomainOrderStatusParameterModel = Crm.Domain.OrderStatus.OrderStatusParameterModel;
using DomainOrderStatusSelectParameterModel = Crm.Domain.OrderStatus.OrderStatusSelectParameterModel;

namespace Crm.Mappers.User.OrderStatus
{
    public static class OrderStatusMapper
    {
        public static PagingModel<OrderStatusModel> MapNew(this (int TotalCount, List<DomainOrderStatusModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderStatusModel>(tuple.List.MapListNew<OrderStatusModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderStatusModel MapNew(this OrderStatusModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderStatusModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderStatusModel MapFrom(this DomainOrderStatusModel domainModel, OrderStatusModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderStatusParameterModel MapNew(this OrderStatusParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderStatusParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderStatusSelectParameterModel MapNew(this int storeId)
        {
            return new DomainOrderStatusSelectParameterModel
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