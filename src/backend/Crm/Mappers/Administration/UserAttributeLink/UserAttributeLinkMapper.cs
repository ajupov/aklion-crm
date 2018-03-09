using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.UserAttributeLink;
using Infrastructure.Mapper;
using DomainUserAttributeLinkModel = Crm.Domain.UserAttributeLink.UserAttributeLinkModel;
using DomainUserAttributeLinkParameterModel = Crm.Domain.UserAttributeLink.UserAttributeLinkParameterModel;

namespace Crm.Mappers.Administration.UserAttributeLink
{
    public static class UserAttributeLinkMapper
    {
        public static PagingModel<UserAttributeLinkModel> MapNew(this (int TotalCount, List<DomainUserAttributeLinkModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeLinkModel>(tuple.List.MapListNew<UserAttributeLinkModel>(), tuple.TotalCount, page, size);
        }

        public static DomainUserAttributeLinkModel MapNew(this UserAttributeLinkModel model)
        {
            return model.MapNew<DomainUserAttributeLinkModel>();
        }

        public static DomainUserAttributeLinkModel MapFrom(this DomainUserAttributeLinkModel domainModel, UserAttributeLinkModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainUserAttributeLinkParameterModel MapNew(this UserAttributeLinkParameterModel model)
        {
            return model.MapParameterNew<DomainUserAttributeLinkParameterModel>();
        }
    }
}