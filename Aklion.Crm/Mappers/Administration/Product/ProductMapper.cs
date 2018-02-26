using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Product;
using Aklion.Infrastructure.Mapper;
using DomainProductAutocompleteParameterModel = Aklion.Crm.Domain.Product.ProductAutocompleteParameterModel;
using DomainProductModel = Aklion.Crm.Domain.Product.ProductModel;
using DomainProductParameterModel = Aklion.Crm.Domain.Product.ProductParameterModel;

namespace Aklion.Crm.Mappers.Administration.Product
{
    public static class ProductMapper
    {
        public static PagingModel<ProductModel> MapNew(this (int TotalCount, List<DomainProductModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductModel>(tuple.List.MapListNew<ProductModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductModel MapNew(this ProductModel model)
        {
            return model.MapNew<DomainProductModel>();
        }

        public static DomainProductModel MapFrom(this DomainProductModel domainModel, ProductModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductParameterModel MapNew(this ProductParameterModel model)
        {
            return model.MapParameterNew<DomainProductParameterModel>();
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