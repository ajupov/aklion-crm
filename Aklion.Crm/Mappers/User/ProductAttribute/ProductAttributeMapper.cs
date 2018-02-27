﻿using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductAttribute;
using Aklion.Infrastructure.Mapper;
using DomainProductAttributeAutocompleteParameterModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeAutocompleteParameterModel;
using DomainProductAttributeModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeModel;
using DomainProductAttributeParameterModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeParameterModel;

namespace Aklion.Crm.Mappers.User.ProductAttribute
{
    public static class ProductAttributeMapper
    {
        public static PagingModel<ProductAttributeModel> MapNew(this (int TotalCount, List<DomainProductAttributeModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductAttributeModel>(tuple.List.MapListNew<ProductAttributeModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductAttributeModel MapNew(this ProductAttributeModel model, int storeId)
        {
            var result = model.MapNew<DomainProductAttributeModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductAttributeModel MapFrom(this DomainProductAttributeModel domainModel, ProductAttributeModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductAttributeParameterModel MapNew(this ProductAttributeParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainProductAttributeParameterModel>();
            result.StoreId = storeId;

            return result;
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