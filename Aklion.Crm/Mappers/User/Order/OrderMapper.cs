using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Order;
using Aklion.Infrastructure.Mapper;
using DomainOrderModel = Aklion.Crm.Domain.Order.OrderModel;
using DomainOrderParameterModel = Aklion.Crm.Domain.Order.OrderParameterModel;

namespace Aklion.Crm.Mappers.User.Order
{
    public static class OrderMapper
    {
        public static PagingModel<OrderModel> MapNew(this (int TotalCount, List<DomainOrderModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderModel>(tuple.List.MapListNew<OrderModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderModel MapNew(this OrderModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderModel MapFrom(this DomainOrderModel domainModel, OrderModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderParameterModel MapNew(this OrderParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}