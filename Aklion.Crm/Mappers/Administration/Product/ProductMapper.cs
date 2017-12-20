using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Product;
using Aklion.Infrastructure.Mapper;
using DomainProductModel = Aklion.Crm.Domain.Product.ProductModel;
using DomainProductParameterModel = Aklion.Crm.Domain.Product.ProductParameterModel;
using DomainProductAutocompleteParameterModel = Aklion.Crm.Domain.Product.ProductAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.Product
{
    public static class ProductMapper
    {
        public static PagingModel<ProductModel> MapNew(this Tuple<int, List<DomainProductModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ProductModel>(tuple.Item2.MapListNew<ProductModel>(), tuple.Item1, page, size);
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
            return model.MapNew<DomainProductParameterModel>();
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