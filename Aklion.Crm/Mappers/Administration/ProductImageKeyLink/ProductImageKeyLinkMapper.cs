using System;
using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductImageKeyLink;
using Aklion.Infrastructure.Mapper;
using DomainProductImageKeyLinkModel = Aklion.Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkModel;
using DomainProductImageKeyLinkParameterModel = Aklion.Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductImageKeyLink
{
    public static class ProductImageKeyLinkMapper
    {
        public static PagingModel<ProductImageKeyLinkModel> MapNew(this (int TotalCount, List<DomainProductImageKeyLinkModel> List) tuple, int? page, int? size)
        {
            var list = tuple.Item2.MapListNew<ProductImageKeyLinkModel>();

            MapImage(tuple.Item2, list);

            return new PagingModel<ProductImageKeyLinkModel>(list, tuple.Item1, page, size);
        }

        public static DomainProductImageKeyLinkModel MapNew(this ProductImageKeyLinkModel model)
        {
            return model.MapNew<DomainProductImageKeyLinkModel>();
        }

        public static DomainProductImageKeyLinkModel MapFrom(this DomainProductImageKeyLinkModel domainModel, ProductImageKeyLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductImageKeyLinkParameterModel MapNew(this ProductImageKeyLinkParameterModel model)
        {
            return model.MapParameterNew<DomainProductImageKeyLinkParameterModel>();
        }

        private static void MapImage(IReadOnlyCollection<DomainProductImageKeyLinkModel> domainList, IEnumerable<ProductImageKeyLinkModel> list)
        {
            foreach (var item in list)
            {
                var domainItem = domainList.FirstOrDefault(i => i.Id == item.Id);
                if (domainItem == null)
                {
                    continue;
                }

                item.Base64Value = Convert.ToBase64String(domainItem.Value);
            }
        }
    }
}