using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainOrderAttributeLinkModel = Aklion.Crm.Domain.OrderAttributeLink.OrderAttributeLinkModel;
using DomainOrderAttributeLinkParameterModel = Aklion.Crm.Domain.OrderAttributeLink.OrderAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.User.OrderAttributeLink
{
    public static class OrderAttributeLinkMapper
    {
        public static PagingModel<OrderAttributeLinkModel> MapNew(this Tuple<int, List<DomainOrderAttributeLinkModel>> tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeLinkModel>(tuple.Item2.MapListNew<OrderAttributeLinkModel>(), tuple.Item1, page, size);
        }

        public static DomainOrderAttributeLinkModel MapNew(this OrderAttributeLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderAttributeLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderAttributeLinkModel MapFrom(this DomainOrderAttributeLinkModel domainModel, OrderAttributeLinkModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderAttributeLinkParameterModel MapNew(this OrderAttributeLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderAttributeLinkParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}