using System;
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
        public static PagingModel<ClientAttributeLinkModel> MapNew(this Tuple<int, List<DomainClientAttributeLinkModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeLinkModel>(tuple.Item2.MapListNew<ClientAttributeLinkModel>(), tuple.Item1, page, size);
        }

        public static DomainClientAttributeLinkModel MapNew(this ClientAttributeLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainClientAttributeLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAttributeLinkModel MapFrom(this DomainClientAttributeLinkModel domainModel, ClientAttributeLinkModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAttributeLinkParameterModel MapNew(this ClientAttributeLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainClientAttributeLinkParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}