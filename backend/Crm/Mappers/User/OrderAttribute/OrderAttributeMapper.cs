using System.Collections.Generic;
using Crm.Models;
using Crm.Models.User.OrderAttribute;
using Infrastructure.Mapper;
using DomainOrderAttributeAutocompleteParameterModel = Crm.Domain.OrderAttribute.OrderAttributeAutocompleteParameterModel;
using DomainOrderAttributeModel = Crm.Domain.OrderAttribute.OrderAttributeModel;
using DomainOrderAttributeParameterModel = Crm.Domain.OrderAttribute.OrderAttributeParameterModel;

namespace Crm.Mappers.User.OrderAttribute
{
    public static class OrderAttributeMapper
    {
        public static PagingModel<OrderAttributeModel> MapNew(this (int TotalCount, List<DomainOrderAttributeModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeModel>(tuple.List.MapListNew<OrderAttributeModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderAttributeModel MapNew(this OrderAttributeModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderAttributeModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderAttributeModel MapFrom(this DomainOrderAttributeModel domainModel, OrderAttributeModel model)
        {
            return Mapper.MapFrom(domainModel, model);
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
                Name = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}