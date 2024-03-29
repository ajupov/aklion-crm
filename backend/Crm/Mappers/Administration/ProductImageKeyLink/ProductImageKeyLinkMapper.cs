﻿using System;
using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.Administration.ProductImageKeyLink;
using Infrastructure.Mapper;
using DomainProductImageKeyLinkModel = Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkModel;
using DomainProductImageKeyLinkParameterModel = Crm.Domain.ProductImageKeyLink.ProductImageKeyLinkParameterModel;

namespace Crm.Mappers.Administration.ProductImageKeyLink
{
    public static class ProductImageKeyLinkMapper
    {
        public static PagingModel<ProductImageKeyLinkModel> MapNew(this (int TotalCount, List<DomainProductImageKeyLinkModel> List) tuple, int? page, int? size)
        {
            var list = tuple.List.MapListNew<ProductImageKeyLinkModel>();

            MapImage(tuple.List, list);

            return new PagingModel<ProductImageKeyLinkModel>(list, tuple.TotalCount, page, size);
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
            if (list == null)
            {
                return;
            }

            foreach (var item in list)
            {
                var domainItem = domainList.FirstOrDefault(i => i.Id == item.Id);
                if (domainItem == null)
                {
                    continue;
                }

                item.Base64Value = domainItem.Value != null ? Convert.ToBase64String(domainItem.Value) : "";
            }
        }
    }
}