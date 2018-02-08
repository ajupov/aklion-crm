using System;
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
        public static PagingModel<OrderModel> MapNew(this Tuple<int, List<DomainOrderModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderModel>(tuple.Item2.MapListNew<OrderModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderModel MapNew(this OrderModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderModel MapFrom(this DomainOrderModel domainModel, OrderModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderParameterModel MapNew(this OrderParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}