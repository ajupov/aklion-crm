using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainClientAttributeLinkModel = Aklion.Crm.Domain.ClientAttributeLink.ClientAttributeLinkModel;
using DomainClientAttributeLinkParameterModel = Aklion.Crm.Domain.ClientAttributeLink.ClientAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.Administration.ClientAttributeLink
{
    public static class ClientAttributeLinkMapper
    {
        public static PagingModel<ClientAttributeLinkModel> MapNew(this Tuple<int, List<DomainClientAttributeLinkModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeLinkModel>(tuple.Item2.MapListNew<ClientAttributeLinkModel>(), tuple.Item1, page, size);
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