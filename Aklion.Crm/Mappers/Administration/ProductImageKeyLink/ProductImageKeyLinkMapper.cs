using System;
using System.Collections.Generic;
using System.IO;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductImageKeyLink;
using Aklion.Infrastructure.Mapper;
using DomainProductImageKeyLinkModel = Aklion.Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkModel;
using DomainProductImageKeyLinkParameterModel = Aklion.Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductImageKeyLink
{
    public static class ProductImageKeyLinkMapper
    {
        public static PagingModel<ProductImageKeyLinkModel> MapNew(this Tuple<int, List<DomainProductImageKeyLinkModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ProductImageKeyLinkModel>(tuple.Item2.MapListNew<ProductImageKeyLinkModel>(), tuple.Item1, page, size);
        }

        public static DomainProductImageKeyLinkModel MapNew(this ProductImageKeyLinkModel model)
        {
            var result = model.MapNew<DomainProductImageKeyLinkModel>();
            model.ImageFile.CopyTo(result.Value);

            return result;
        }

        public static DomainProductImageKeyLinkModel MapFrom(this DomainProductImageKeyLinkModel domainModel, ProductImageKeyLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductImageKeyLinkParameterModel MapNew(this ProductImageKeyLinkParameterModel model)
        {
            return model.MapParameterNew<DomainProductImageKeyLinkParameterModel>();
        }
    }
}