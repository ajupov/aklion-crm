using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttribute;
using Aklion.Infrastructure.Mapper;
using DomainOrderAttributeModel = Aklion.Crm.Domain.OrderAttribute.OrderAttributeModel;
using DomainOrderAttributeParameterModel = Aklion.Crm.Domain.OrderAttribute.OrderAttributeParameterModel;
using DomainOrderAttributeAutocompleteParameterModel = Aklion.Crm.Domain.OrderAttribute.OrderAttributeAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.OrderAttribute
{
    public static class OrderAttributeMapper
    {
        public static PagingModel<OrderAttributeModel> MapNew(this Tuple<int, List<DomainOrderAttributeModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeModel>(tuple.Item2.MapListNew<OrderAttributeModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderAttributeModel MapNew(this OrderAttributeModel model)
        {
            return model.MapNew<DomainOrderAttributeModel>();
        }

        public static DomainOrderAttributeModel MapFrom(this DomainOrderAttributeModel domainModel, OrderAttributeModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderAttributeParameterModel MapNew(this OrderAttributeParameterModel model)
        {
            return model.MapParameterNew<DomainOrderAttributeParameterModel>();
        }

        public static DomainOrderAttributeAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainOrderAttributeAutocompleteParameterModel
            {
                Description = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}