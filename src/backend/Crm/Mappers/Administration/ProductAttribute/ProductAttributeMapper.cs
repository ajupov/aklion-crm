using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.ProductAttribute;
using Infrastructure.Mapper;
using DomainProductAttributeAutocompleteParameterModel = Crm.Domain.ProductAttribute.ProductAttributeAutocompleteParameterModel;
using DomainProductAttributeModel = Crm.Domain.ProductAttribute.ProductAttributeModel;
using DomainProductAttributeParameterModel = Crm.Domain.ProductAttribute.ProductAttributeParameterModel;

namespace Crm.Mappers.Administration.ProductAttribute
{
    public static class ProductAttributeMapper
    {
        public static PagingModel<ProductAttributeModel> MapNew(this (int TotalCount, List<DomainProductAttributeModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductAttributeModel>(tuple.List.MapListNew<ProductAttributeModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductAttributeModel MapNew(this ProductAttributeModel model)
        {
            return model.MapNew<DomainProductAttributeModel>();
        }

        public static DomainProductAttributeModel MapFrom(this DomainProductAttributeModel domainModel, ProductAttributeModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductAttributeParameterModel MapNew(this ProductAttributeParameterModel model)
        {
            return model.MapParameterNew<DomainProductAttributeParameterModel>();
        }

        public static DomainProductAttributeAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainProductAttributeAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}