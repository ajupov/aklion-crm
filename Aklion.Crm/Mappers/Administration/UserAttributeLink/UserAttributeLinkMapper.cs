using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttributeLink;
using Aklion.Infrastructure.Mapper;
using DomainUserAttributeLinkModel = Aklion.Crm.Domain.UserAttributeLink.UserAttributeLinkModel;
using DomainUserAttributeLinkParameterModel = Aklion.Crm.Domain.UserAttributeLink.UserAttributeLinkParameterModel;

namespace Aklion.Crm.Mappers.Administration.UserAttributeLink
{
    public static class UserAttributeLinkMapper
    {
        public static PagingModel<UserAttributeLinkModel> MapNew(this Tuple<int, List<DomainUserAttributeLinkModel>> tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeLinkModel>(tuple.Item2.MapListNew<UserAttributeLinkModel>(), tuple.Item1, page, size);
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