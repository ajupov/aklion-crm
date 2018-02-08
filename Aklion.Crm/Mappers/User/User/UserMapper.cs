using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.User;
using Aklion.Infrastructure.Mapper;
using DomainUserModel = Aklion.Crm.Domain.User.UserModel;
using DomainUserParameterModel = Aklion.Crm.Domain.User.UserParameterModel;
using DomainUserAutocompleteParameterModel = Aklion.Crm.Domain.User.UserAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.User.User
{
    public static class UserMapper
    {
        public static PagingModel<UserModel> MapNew(this Tuple<int, List<DomainUserModel>> tuple, int? page, int? size)
        {
            return new PagingModel<UserModel>(tuple.Item2.MapListNew<UserModel>(), tuple.Item1, page, size);
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