using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductStatus;
using Aklion.Infrastructure.Mapper;
using DomainProductStatusModel = Aklion.Crm.Domain.ProductStatus.ProductStatusModel;
using DomainProductStatusParameterModel = Aklion.Crm.Domain.ProductStatus.ProductStatusParameterModel;
using DomainProductStatusSelectParameterModel = Aklion.Crm.Domain.ProductStatus.ProductStatusSelectParameterModel;

namespace Aklion.Crm.Mappers.User.ProductStatus
{
    public static class ProductStatusMapper
    {
        public static PagingModel<ProductStatusModel> MapNew(this (int TotalCount, List<DomainProductStatusModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductStatusModel>(tuple.List.MapListNew<ProductStatusModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductStatusModel MapNew(this ProductStatusModel model, int storeId)
        {
            var result = model.MapNew<DomainProductStatusModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductStatusModel MapFrom(this DomainProductStatusModel domainModel, ProductStatusModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductStatusParameterModel MapNew(this ProductStatusParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainProductStatusParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainProductStatusSelectParameterModel MapNew(this int storeId)
        {
            return new DomainProductStatusSelectParameterModel
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