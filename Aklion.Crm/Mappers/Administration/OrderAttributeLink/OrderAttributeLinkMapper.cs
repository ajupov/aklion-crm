using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainOrderAttributeLinkModel = Aklion.Crm.Domain.OrderAttributeLink.OrderAttributeLinkModel;
using DomainOrderAttributeLinkParameterModel = Aklion.Crm.Domain.OrderAttributeLink.OrderAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.Administration.OrderAttributeLink
{
    public static class OrderAttributeLinkMapper
    {
        public static PagingModel<OrderAttributeLinkModel> MapNew(
            this (int TotalCount, List<DomainOrderAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<OrderAttributeLinkModel>(tuple.List.MapListNew<OrderAttributeLinkModel>(),
                tuple.TotalCount, page, size);
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