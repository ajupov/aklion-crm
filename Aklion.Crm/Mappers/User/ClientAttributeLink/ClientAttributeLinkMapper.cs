using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ClientAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainClientAttributeLinkModel = Aklion.Crm.Domain.ClientAttributeLink.ClientAttributeLinkModel;
using DomainClientAttributeLinkParameterModel = Aklion.Crm.Domain.ClientAttributeLink.ClientAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.User.ClientAttributeLink
{
    public static class ClientAttributeLinkMapper
    {
        public static PagingModel<ClientAttributeLinkModel> MapNew(this (int TotalCount, List<DomainClientAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeLinkModel>(tuple.List.MapListNew<ClientAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainClientAttributeLinkModel MapNew(this ClientAttributeLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainClientAttributeLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAttributeLinkModel MapFrom(this DomainClientAttributeLinkModel domainModel, ClientAttributeLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainClientAttributeLinkParameterModel MapNew(this ClientAttributeLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainClientAttributeLinkParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}