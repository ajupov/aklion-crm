﻿using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.Administration.Order;
using Infrastructure.Mapper;
using DomainOrderModel = Crm.Domain.Order.OrderModel;
using DomainOrderParameterModel = Crm.Domain.Order.OrderParameterModel;

namespace Crm.Mappers.Administration.Order
{
    public static class OrderMapper
    {
        public static PagingModel<OrderModel> MapNew(this (int TotalCount, List<DomainOrderModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderModel>(tuple.List.MapListNew<OrderModel>(), tuple.TotalCount, page, size);
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
            return model.MapParameterNew<DomainOrderParameterModel>();
        }

        public static Dictionary<int, int> MapNew(this List<int> models)
        {
            return models.ToDictionary(m => m, m => m);
        }
    }
}