using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserAttribute;
using Aklion.Infrastructure.Mapper;
using DomainUserAttributeModel = Aklion.Crm.Domain.UserAttribute.UserAttributeModel;
using DomainUserAttributeParameterModel = Aklion.Crm.Domain.UserAttribute.UserAttributeParameterModel;
using DomainUserAttributeAutocompleteParameterModel = Aklion.Crm.Domain.UserAttribute.UserAttributeAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.User.UserAttribute
{
    public static class UserAttributeMapper
    {
        public static PagingModel<UserAttributeModel> MapNew(this Tuple<int, List<DomainUserAttributeModel>> tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeModel>(tuple.Item2.MapListNew<UserAttributeModel>(), tuple.Item1, page, size);
        }

        public static DomainUserAttributeModel MapNew(this UserAttributeModel model, int storeId)
        {
            var result = model.MapNew<DomainUserAttributeModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserAttributeModel MapFrom(this DomainUserAttributeModel domainModel, UserAttributeModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserAttributeParameterModel MapNew(this UserAttributeParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainUserAttributeParameterModel>();
            result.StoreId = storeId;

            return result;
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