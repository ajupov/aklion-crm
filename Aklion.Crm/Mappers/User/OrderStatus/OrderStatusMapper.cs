using System;
using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderStatus;
using Aklion.Infrastructure.Mapper;
using DomainOrderStatusModel = Aklion.Crm.Domain.OrderStatus.OrderStatusModel;
using DomainOrderStatusParameterModel = Aklion.Crm.Domain.OrderStatus.OrderStatusParameterModel;
using DomainOrderStatusSelectParameterModel = Aklion.Crm.Domain.OrderStatus.OrderStatusSelectParameterModel;

namespace Aklion.Crm.Mappers.User.OrderStatus
{
    public static class OrderStatusMapper
    {
        public static PagingModel<OrderStatusModel> MapNew(this Tuple<int, List<DomainOrderStatusModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderStatusModel>(tuple.Item2.MapListNew<OrderStatusModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderStatusModel MapNew(this OrderStatusModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderStatusModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderStatusModel MapFrom(this DomainOrderStatusModel domainModel, OrderStatusModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
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