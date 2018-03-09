using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.User;
using Infrastructure.Mapper;
using DomainUserAutocompleteParameterModel = Crm.Domain.User.UserAutocompleteParameterModel;
using DomainUserModel = Crm.Domain.User.UserModel;
using DomainUserParameterModel = Crm.Domain.User.UserParameterModel;

namespace Crm.Mappers.Administration.User
{
    public static class UserMapper
    {
        public static PagingModel<UserModel> MapNew(this (int TotalCount, List<DomainUserModel> List) tuple, int? page, int? size)
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

        public static DomainUserParameterModel MapNew(this UserParameterModel model)
        {
            return model.MapParameterNew<DomainUserParameterModel>();
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