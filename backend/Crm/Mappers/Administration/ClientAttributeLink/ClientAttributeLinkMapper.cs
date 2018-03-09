using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.ClientAttributeLink;
using Infrastructure.Mapper;
using DomainClientAttributeLinkModel = Crm.Domain.ClientAttributeLink.ClientAttributeLinkModel;
using DomainClientAttributeLinkParameterModel = Crm.Domain.ClientAttributeLink.ClientAttributeLinkParameterModel;

namespace Crm.Mappers.Administration.ClientAttributeLink
{
    public static class ClientAttributeLinkMapper
    {
        public static PagingModel<ClientAttributeLinkModel> MapNew(this (int TotalCount, List<DomainClientAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeLinkModel>(tuple.List.MapListNew<ClientAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainClientAttributeLinkModel MapNew(this ClientAttributeLinkModel model)
        {
            return model.MapNew<DomainClientAttributeLinkModel>();
        }

        public static DomainClientAttributeLinkModel MapFrom(this DomainClientAttributeLinkModel domainModel, ClientAttributeLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainClientAttributeLinkParameterModel MapNew(this ClientAttributeLinkParameterModel model)
        {
            return model.MapParameterNew<DomainClientAttributeLinkParameterModel>();
        }
    }
}