using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttribute;
using Aklion.Infrastructure.Mapper;
using DomainUserAttributeModel = Aklion.Crm.Domain.UserAttribute.UserAttributeModel;
using DomainUserAttributeParameterModel = Aklion.Crm.Domain.UserAttribute.UserAttributeParameterModel;
using DomainUserAttributeAutocompleteParameterModel = Aklion.Crm.Domain.UserAttribute.UserAttributeAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.UserAttribute
{
    public static class UserAttributeMapper
    {
        public static PagingModel<UserAttributeModel> MapNew(this Tuple<int, List<DomainUserAttributeModel>> tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeModel>(tuple.Item2.MapListNew<UserAttributeModel>(), tuple.Item1, page, size);
        }

        public static DomainUserAttributeModel MapNew(this UserAttributeModel model)
        {
            return model.MapNew<DomainUserAttributeModel>();
        }

        public static DomainUserAttributeModel MapFrom(this DomainUserAttributeModel domainModel, UserAttributeModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainUserAttributeParameterModel MapNew(this UserAttributeParameterModel model)
        {
            return model.MapNew<DomainUserAttributeParameterModel>();
        }

        public static DomainUserAttributeAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainUserAttributeAutocompleteParameterModel
            {
                Description = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}