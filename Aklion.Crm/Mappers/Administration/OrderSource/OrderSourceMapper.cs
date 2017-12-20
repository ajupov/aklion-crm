using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderSource;
using Aklion.Infrastructure.Mapper;
using DomainOrderSourceModel = Aklion.Crm.Domain.OrderSource.OrderSourceModel;
using DomainOrderSourceParameterModel = Aklion.Crm.Domain.OrderSource.OrderSourceParameterModel;
using DomainOrderSourceAutocompleteParameterModel = Aklion.Crm.Domain.OrderSource.OrderSourceAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.OrderSource
{
    public static class OrderSourceMapper
    {
        public static PagingModel<OrderSourceModel> MapNew(this Tuple<int, List<DomainOrderSourceModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderSourceModel>(tuple.Item2.MapListNew<OrderSourceModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderSourceModel MapNew(this OrderSourceModel model)
        {
            return model.MapNew<DomainOrderSourceModel>();
        }

        public static DomainOrderSourceModel MapFrom(this DomainOrderSourceModel domainModel, OrderSourceModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderSourceParameterModel MapNew(this OrderSourceParameterModel model)
        {
            return model.MapNew<DomainOrderSourceParameterModel>();
        }

        public static DomainOrderSourceAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainOrderSourceAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId
            };
        }
    }
}