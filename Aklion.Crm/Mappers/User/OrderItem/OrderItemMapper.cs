using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderItem;
using Aklion.Infrastructure.Mapper;
using DomainOrderItemModel = Aklion.Crm.Domain.OrderItem.OrderItemModel;
using DomainOrderItemParameterModel = Aklion.Crm.Domain.OrderItem.OrderItemParameterModel;

namespace Aklion.Crm.Mappers.User.OrderItem
{
    public static class OrderItemMapper
    {
        public static PagingModel<OrderItemModel> MapNew(this Tuple<int, List<DomainOrderItemModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderItemModel>(tuple.Item2.MapListNew<OrderItemModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderItemModel MapNew(this OrderItemModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderItemModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderItemModel MapFrom(this DomainOrderItemModel domainModel, OrderItemModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderItemParameterModel MapNew(this OrderItemParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderItemParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}