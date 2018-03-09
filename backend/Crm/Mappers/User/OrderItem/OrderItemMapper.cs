using System.Collections.Generic;
using Crm.Models;
using Crm.Models.User.OrderItem;
using Infrastructure.Mapper;
using DomainOrderItemModel = Crm.Domain.OrderItem.OrderItemModel;
using DomainOrderItemParameterModel = Crm.Domain.OrderItem.OrderItemParameterModel;

namespace Crm.Mappers.User.OrderItem
{
    public static class OrderItemMapper
    {
        public static PagingModel<OrderItemModel> MapNew(this (int TotalCount, List<DomainOrderItemModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderItemModel>(tuple.List.MapListNew<OrderItemModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderItemModel MapNew(this OrderItemModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderItemModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderItemModel MapFrom(this DomainOrderItemModel domainModel, OrderItemModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderItemParameterModel MapNew(this OrderItemParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderItemParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}