using System.Collections.Generic;
using Crm.Models;
using Crm.Models.User.OrderAttributeLink;
using Infrastructure.Mapper;
using DomainOrderAttributeLinkModel = Crm.Domain.OrderAttributeLink.OrderAttributeLinkModel;
using DomainOrderAttributeLinkParameterModel = Crm.Domain.OrderAttributeLink.OrderAttributeLinkParameterModel;

namespace Crm.Mappers.User.OrderAttributeLink
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