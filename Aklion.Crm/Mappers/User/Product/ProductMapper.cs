using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Product;
using Aklion.Infrastructure.Mapper;
using DomainProductAutocompleteParameterModel = Aklion.Crm.Domain.Product.ProductAutocompleteParameterModel;
using DomainProductModel = Aklion.Crm.Domain.Product.ProductModel;
using DomainProductParameterModel = Aklion.Crm.Domain.Product.ProductParameterModel;

namespace Aklion.Crm.Mappers.User.Product
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