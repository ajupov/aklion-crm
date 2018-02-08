using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderAttribute;
using Aklion.Infrastructure.Mapper;
using DomainOrderAttributeModel = Aklion.Crm.Domain.OrderAttribute.OrderAttributeModel;
using DomainOrderAttributeParameterModel = Aklion.Crm.Domain.OrderAttribute.OrderAttributeParameterModel;
using DomainOrderAttributeAutocompleteParameterModel = Aklion.Crm.Domain.OrderAttribute.OrderAttributeAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.User.OrderAttribute
{
    public static class OrderAttributeMapper
    {
        public static PagingModel<OrderAttributeModel> MapNew(this Tuple<int, List<DomainOrderAttributeModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeModel>(tuple.Item2.MapListNew<OrderAttributeModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderAttributeModel MapNew(this OrderAttributeModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderAttributeModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderAttributeModel MapFrom(this DomainOrderAttributeModel domainModel, OrderAttributeModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderAttributeParameterModel MapNew(this OrderAttributeParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderAttributeParameterModel>();
            result.StoreId = storeId;

            return result;
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