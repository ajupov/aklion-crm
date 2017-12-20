using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttribute;
using Aklion.Infrastructure.Mapper;
using DomainProductAttributeModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeModel;
using DomainProductAttributeParameterModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeParameterModel;
using DomainProductAttributeAutocompleteParameterModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductAttribute
{
    public static class ProductAttributeMapper
    {
        public static PagingModel<ProductAttributeModel> MapNew(this Tuple<int, List<DomainProductAttributeModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ProductAttributeModel>(tuple.Item2.MapListNew<ProductAttributeModel>(), tuple.Item1, page, size);
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
            return model.MapNew<DomainProductAttributeParameterModel>();
        }

        public static DomainProductAttributeAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainProductAttributeAutocompleteParameterModel
            {
                Description = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}