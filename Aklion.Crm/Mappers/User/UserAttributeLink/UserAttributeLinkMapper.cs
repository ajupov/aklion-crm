using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainUserAttributeLinkModel = Aklion.Crm.Domain.UserAttributeLink.UserAttributeLinkModel;
using DomainUserAttributeLinkParameterModel = Aklion.Crm.Domain.UserAttributeLink.UserAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.User.UserAttributeLink
{
    public static class UserAttributeLinkMapper
    {
        public static PagingModel<UserAttributeLinkModel> MapNew(this Tuple<int, List<DomainUserAttributeLinkModel>> tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeLinkModel>(tuple.Item2.MapListNew<UserAttributeLinkModel>(), tuple.Item1, page, size);
        }

        public static DomainUserAttributeLinkModel MapNew(this UserAttributeLinkModel model, int storeId)
        {
            var result = model.MapNew<DomainUserAttributeLinkModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserAttributeLinkModel MapFrom(this DomainUserAttributeLinkModel domainModel, UserAttributeLinkModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserAttributeLinkParameterModel MapNew(this UserAttributeLinkParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainUserAttributeLinkParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}