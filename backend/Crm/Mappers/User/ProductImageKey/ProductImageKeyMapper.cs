﻿using System;
using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.User.ProductImageKey;
using Infrastructure.Mapper;
using DomainProductImageKeyModel = Crm.Domain.ProductImageKey.ProductImageKeyModel;
using DomainProductImageKeyParameterModel = Crm.Domain.ProductImageKey.ProductImageKeyParameterModel;
using DomainProductImageKeySelectParameterModel = Crm.Domain.ProductImageKey.ProductImageKeySelectParameterModel;

namespace Crm.Mappers.User.ProductImageKey
{
    public static class ProductImageKeyMapper
    {
        public static PagingModel<ProductImageKeyModel> MapNew(this (int TotalCount, List<DomainProductImageKeyModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductImageKeyModel>(tuple.List.MapListNew<ProductImageKeyModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductImageKeyModel MapNew(this ProductImageKeyModel model, int storeId)
        {
            var result = model.MapNew<DomainProductImageKeyModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductImageKeyModel MapFrom(this DomainProductImageKeyModel domainModel, ProductImageKeyModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductImageKeyParameterModel MapNew(this ProductImageKeyParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainProductImageKeyParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductImageKeySelectParameterModel MapNew(this int storeId)
        {
            return new DomainProductImageKeySelectParameterModel
            {
                StoreId = storeId
            };
        }

        public static Dictionary<string, int> MapNew(this Dictionary<string, int> models)
        {
            models.TryAdd(string.Empty, 0);

            return models.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}