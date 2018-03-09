using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.UserAttribute;
using Infrastructure.Mapper;
using DomainUserAttributeAutocompleteParameterModel = Crm.Domain.UserAttribute.UserAttributeAutocompleteParameterModel;
using DomainUserAttributeModel = Crm.Domain.UserAttribute.UserAttributeModel;
using DomainUserAttributeParameterModel = Crm.Domain.UserAttribute.UserAttributeParameterModel;

namespace Crm.Mappers.Administration.UserAttribute
{
    public static class UserAttributeMapper
    {
        public static PagingModel<UserAttributeModel> MapNew(this (int TotalCount, List<DomainUserAttributeModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<UserAttributeModel>(tuple.List.MapListNew<UserAttributeModel>(), tuple.TotalCount, page, size);
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
            return model.MapParameterNew<DomainUserAttributeParameterModel>();
        }

        public static DomainUserAttributeAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainUserAttributeAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}