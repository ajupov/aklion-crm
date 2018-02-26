﻿using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainProductAttributeLinkModel = Aklion.Crm.Domain.ProductAttributeLink.ProductAttributeLinkModel;
using DomainProductAttributeLinkParameterModel = Aklion.Crm.Domain.ProductAttributeLink.ProductAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductAttributeLink
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