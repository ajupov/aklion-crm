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
        public static PagingModel<OrderAttributeLinkModel> MapNew(this (int TotalCount, List<DomainOrderAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeLinkModel>(tuple.List.MapListNew<OrderAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderAttributeLinkModel MapNew(this OrderAttributeLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainOrderAttributeLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainOrderAttributeLinkModel MapFrom(this DomainOrderAttributeLinkModel domainModel, OrderAttributeLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderAttributeLinkParameterModel MapNew(this OrderAttributeLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainOrderAttributeLinkParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}