using System.Collections.Generic;
using Crm.Models;
using Crm.Models.User.UserAttributeLink;
using Infrastructure.Mapper;
using DomainUserAttributeLinkModel = Crm.Domain.UserAttributeLink.UserAttributeLinkModel;
using DomainUserAttributeLinkParameterModel = Crm.Domain.UserAttributeLink.UserAttributeLinkParameterModel;

namespace Crm.Mappers.User.UserAttributeLink
{
    public static class UserAttributeLinkMapper
    {
        public static PagingModel<UserAttributeLinkModel> MapNew(this (int TotalCount, List<DomainUserAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeLinkModel>(tuple.List.MapListNew<UserAttributeLinkModel>(), tuple.TotalCount, page, size);
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