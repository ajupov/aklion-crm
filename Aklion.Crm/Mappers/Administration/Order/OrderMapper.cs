using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Order;
using Aklion.Infrastructure.Mapper;
using DomainOrderModel = Aklion.Crm.Domain.Order.OrderModel;
using DomainOrderParameterModel = Aklion.Crm.Domain.Order.OrderParameterModel;

namespace Aklion.Crm.Mappers.Administration.Order
{
    public static class OrderMapper
    {
        public static PagingModel<OrderModel> MapNew(this Tuple<int, List<DomainOrderModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderModel>(tuple.Item2.MapListNew<OrderModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderModel MapNew(this OrderModel model)
        {
            return model.MapNew<DomainOrderModel>();
        }

        public static DomainOrderModel MapFrom(this DomainOrderModel domainModel, OrderModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderParameterModel MapNew(this OrderParameterModel model)
        {
            return model.MapNew<DomainOrderParameterModel>();
        }
    }
}