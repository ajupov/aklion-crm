using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.ProductAttributeLink;
using Infrastructure.Mapper;
using DomainProductAttributeLinkModel = Crm.Domain.ProductAttributeLink.ProductAttributeLinkModel;
using DomainProductAttributeLinkParameterModel = Crm.Domain.ProductAttributeLink.ProductAttributeLinkParameterModel;

namespace Crm.Mappers.Administration.ProductAttributeLink
{
    public static class ProductAttributeLinkMapper
    {
        public static PagingModel<ProductAttributeLinkModel> MapNew(this (int TotalCount, List<DomainProductAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductAttributeLinkModel>(tuple.List.MapListNew<ProductAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductAttributeLinkModel MapNew(this ProductAttributeLinkModel model)
        {
            return model.MapNew<DomainProductAttributeLinkModel>();
        }

        public static DomainProductAttributeLinkModel MapFrom(this DomainProductAttributeLinkModel domainModel, ProductAttributeLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductAttributeLinkParameterModel MapNew(this ProductAttributeLinkParameterModel model)
        {
            return model.MapParameterNew<DomainProductAttributeLinkParameterModel>();
        }
    }
}