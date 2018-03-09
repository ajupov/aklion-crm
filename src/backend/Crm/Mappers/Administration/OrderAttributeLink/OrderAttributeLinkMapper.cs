using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.OrderAttributeLink;
using Infrastructure.Mapper;
using DomainOrderAttributeLinkModel = Crm.Domain.OrderAttributeLink.OrderAttributeLinkModel;
using DomainOrderAttributeLinkParameterModel = Crm.Domain.OrderAttributeLink.OrderAttributeLinkParameterModel;

namespace Crm.Mappers.Administration.OrderAttributeLink
{
    public static class OrderAttributeLinkMapper
    {
        public static PagingModel<OrderAttributeLinkModel> MapNew(this (int TotalCount, List<DomainOrderAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeLinkModel>(tuple.List.MapListNew<OrderAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainOrderAttributeLinkModel MapNew(this OrderAttributeLinkModel model)
        {
            return model.MapNew<DomainOrderAttributeLinkModel>();
        }

        public static DomainOrderAttributeLinkModel MapFrom(this DomainOrderAttributeLinkModel domainModel,
            OrderAttributeLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainOrderAttributeLinkParameterModel MapNew(this OrderAttributeLinkParameterModel model)
        {
            return model.MapParameterNew<DomainOrderAttributeLinkParameterModel>();
        }
    }
}