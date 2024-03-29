﻿using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.Administration.ProductImageKey;
using Infrastructure.Mapper;
using DomainProductImageKeyModel = Crm.Domain.ProductImageKey.ProductImageKeyModel;
using DomainProductImageKeyParameterModel = Crm.Domain.ProductImageKey.ProductImageKeyParameterModel;
using DomainProductImageKeySelectParameterModel = Crm.Domain.ProductImageKey.ProductImageKeySelectParameterModel;

namespace Crm.Mappers.Administration.ProductImageKey
{
    public static class ProductImageKeyMapper
    {
        public static PagingModel<ProductImageKeyModel> MapNew(this (int TotalCount, List<DomainProductImageKeyModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductImageKeyModel>(tuple.Item2.MapListNew<ProductImageKeyModel>(), tuple.Item1, page, size);
        }

        public static DomainProductImageKeyModel MapNew(this ProductImageKeyModel model)
        {
            return model.MapNew<DomainProductImageKeyModel>();
        }

        public static DomainProductImageKeyModel MapFrom(this DomainProductImageKeyModel domainModel, ProductImageKeyModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductImageKeyParameterModel MapNew(this ProductImageKeyParameterModel model)
        {
            return model.MapParameterNew<DomainProductImageKeyParameterModel>();
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