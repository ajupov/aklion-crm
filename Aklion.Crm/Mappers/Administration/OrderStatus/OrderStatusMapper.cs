using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderStatus;
using Aklion.Infrastructure.Mapper;
using DomainOrderStatusModel = Aklion.Crm.Domain.OrderStatus.OrderStatusModel;
using DomainOrderStatusParameterModel = Aklion.Crm.Domain.OrderStatus.OrderStatusParameterModel;
using DomainOrderStatusAutocompleteParameterModel = Aklion.Crm.Domain.OrderStatus.OrderStatusAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.OrderStatus
{
    public static class OrderStatusMapper
    {
        public static PagingModel<OrderStatusModel> MapNew(this Tuple<int, List<DomainOrderStatusModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderStatusModel>(tuple.Item2.MapListNew<OrderStatusModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderStatusModel MapNew(this OrderStatusModel model)
        {
            return model.MapNew<DomainOrderStatusModel>();
        }

        public static DomainOrderStatusModel MapFrom(this DomainOrderStatusModel domainModel, OrderStatusModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderStatusParameterModel MapNew(this OrderStatusParameterModel model)
        {
            return model.MapNew<DomainOrderStatusParameterModel>();
        }

        public static DomainOrderStatusAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainOrderStatusAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId
            };
        }
    }
}