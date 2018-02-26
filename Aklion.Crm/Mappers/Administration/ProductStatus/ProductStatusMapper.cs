using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductStatus;
using Aklion.Infrastructure.Mapper;
using DomainProductStatusModel = Aklion.Crm.Domain.ProductStatus.ProductStatusModel;
using DomainProductStatusParameterModel = Aklion.Crm.Domain.ProductStatus.ProductStatusParameterModel;
using DomainProductStatusSelectParameterModel = Aklion.Crm.Domain.ProductStatus.ProductStatusSelectParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductStatus
{
    public static class ProductStatusMapper
    {
        public static PagingModel<ProductStatusModel> MapNew(this (int TotalCount, List<DomainProductStatusModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ProductStatusModel>(tuple.List.MapListNew<ProductStatusModel>(), tuple.TotalCount, page, size);
        }

        public static DomainProductStatusModel MapNew(this ProductStatusModel model)
        {
            return model.MapNew<DomainProductStatusModel>();
        }

        public static DomainProductStatusModel MapFrom(this DomainProductStatusModel domainModel, ProductStatusModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainProductStatusParameterModel MapNew(this ProductStatusParameterModel model)
        {
            return model.MapParameterNew<DomainProductStatusParameterModel>();
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