using System.Collections.Generic;
using Crm.Models;
using Crm.Models.User.Product;
using Infrastructure.Mapper;
using DomainProductAutocompleteParameterModel = Crm.Domain.Product.ProductAutocompleteParameterModel;
using DomainProductModel = Crm.Domain.Product.ProductModel;
using DomainProductParameterModel = Crm.Domain.Product.ProductParameterModel;

namespace Crm.Mappers.User.Product
{
    public static class ProductMapper
    {
        public static PagingModel<ProductModel> MapNew(this (int TotalCount, List<DomainProductModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductModel>(tuple.List.MapListNew<ProductModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductModel MapNew(this ProductModel model, int storeId)
        {
            var result = model.MapNew<DomainProductModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductModel MapFrom(this DomainProductModel domainModel, ProductModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductParameterModel MapNew(this ProductParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainProductParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainProductAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}