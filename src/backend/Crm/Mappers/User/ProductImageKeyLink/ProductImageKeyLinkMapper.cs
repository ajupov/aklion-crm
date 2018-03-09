using System;
using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.User.ProductImageKeyLink;
using Infrastructure.Mapper;
using DomainProductImageKeyLinkModel = Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkModel;
using DomainProductImageKeyLinkParameterModel = Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkParameterModel;

namespace Crm.Mappers.User.ProductImageKeyLink
{
    public static class ProductImageKeyLinkMapper
    {
        public static PagingModel<ProductImageKeyLinkModel> MapNew(this (int TotalCount, List<DomainProductImageKeyLinkModel> List) tuple, int? page, int? size)
        {
            var list = tuple.List.MapListNew<ProductImageKeyLinkModel>();

            MapImage(tuple.List, list);

            return new PagingModel<ProductImageKeyLinkModel>(list, tuple.TotalCount, page, size);
        }

        public static DomainProductImageKeyLinkModel MapNew(this ProductImageKeyLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainProductImageKeyLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductImageKeyLinkModel MapFrom(this DomainProductImageKeyLinkModel domainModel, ProductImageKeyLinkModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductImageKeyLinkParameterModel MapNew(this ProductImageKeyLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainProductImageKeyLinkParameterModel>();
            result.StoreId = storeId;

            return result;
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