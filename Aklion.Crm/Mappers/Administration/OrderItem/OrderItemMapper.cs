using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderItem;
using Aklion.Infrastructure.Mapper;
using DomainOrderItemModel = Aklion.Crm.Domain.OrderItem.OrderItemModel;
using DomainOrderItemParameterModel = Aklion.Crm.Domain.OrderItem.OrderItemParameterModel;

namespace Aklion.Crm.Mappers.Administration.OrderItem
{
    public static class OrderItemMapper
    {
        public static PagingModel<OrderItemModel> MapNew(this (int TotalCount, List<DomainOrderItemModel> List) tuple,
            int? page, int? size)
        {
            return new PagingModel<OrderItemModel>(tuple.List.MapListNew<OrderItemModel>(), tuple.TotalCount, page,
                size);
        }

        public static DomainOrderItemModel MapNew(this OrderItemModel model)
        {
            return model.MapNew<DomainOrderItemModel>();
        }

        public static DomainOrderItemModel MapFrom(this DomainOrderItemModel domainModel, OrderItemModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderItemParameterModel MapNew(this OrderItemParameterModel model)
        {
            return model.MapParameterNew<DomainOrderItemParameterModel>();
        }
    }
}