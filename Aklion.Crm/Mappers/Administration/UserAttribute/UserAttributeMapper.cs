using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttribute;
using Aklion.Infrastructure.Mapper;
using DomainUserAttributeAutocompleteParameterModel = Aklion.Crm.Domain.UserAttribute.UserAttributeAutocompleteParameterModel;
using DomainUserAttributeModel = Aklion.Crm.Domain.UserAttribute.UserAttributeModel;
using DomainUserAttributeParameterModel = Aklion.Crm.Domain.UserAttribute.UserAttributeParameterModel;

namespace Aklion.Crm.Mappers.Administration.UserAttribute
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