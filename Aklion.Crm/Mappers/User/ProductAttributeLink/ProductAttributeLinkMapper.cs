using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainProductAttributeLinkModel = Aklion.Crm.Domain.ProductAttributeLink.ProductAttributeLinkModel;
using DomainProductAttributeLinkParameterModel = Aklion.Crm.Domain.ProductAttributeLink.ProductAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.User.ProductAttributeLink
{
    public static class ProductAttributeLinkMapper
    {
        public static PagingModel<ProductAttributeLinkModel> MapNew(this (int TotalCount, List<DomainProductAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductAttributeLinkModel>(tuple.List.MapListNew<ProductAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductAttributeLinkModel MapNew(this ProductAttributeLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainProductAttributeLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductAttributeLinkModel MapFrom(this DomainProductAttributeLinkModel domainModel, ProductAttributeLinkModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductAttributeLinkParameterModel MapNew(this ProductAttributeLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainProductAttributeLinkParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}