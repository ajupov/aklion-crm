using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.User;
using Aklion.Infrastructure.Mapper;
using DomainUserAutocompleteParameterModel = Aklion.Crm.Domain.User.UserAutocompleteParameterModel;
using DomainUserByStoreModel = Aklion.Crm.Domain.User.UserByStoreModel;
using DomainUserModel = Aklion.Crm.Domain.User.UserModel;
using DomainUserParameterModel = Aklion.Crm.Domain.User.UserByStoreParameterModel;

namespace Aklion.Crm.Mappers.User.User
{
    public static class UserMapper
    {
        public static PagingModel<UserModel> MapNew(this (int TotalCount, List<DomainUserByStoreModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<UserModel>(tuple.List.MapListNew<UserModel>(), tuple.TotalCount, page, size);
        }

        public static DomainUserModel MapNew(this UserModel model)
        {
            return model.MapNew<DomainUserModel>();
        }

        public static DomainUserModel MapFrom(this DomainUserModel domainModel, UserModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainUserParameterModel MapNew(this UserParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainUserParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserAutocompleteParameterModel MapNew(this string pattern)
        {
            return new DomainUserAutocompleteParameterModel
            {
                Login = pattern,
                IsDeleted = false
            };
        }
    }
}