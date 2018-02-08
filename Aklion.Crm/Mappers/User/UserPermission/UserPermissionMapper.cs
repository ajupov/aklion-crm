using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserPermission;
using Aklion.Infrastructure.Mapper;
using DomainUserPermissionModel = Aklion.Crm.Domain.UserPermission.UserPermissionModel;
using DomainUserPermissionParameterModel = Aklion.Crm.Domain.UserPermission.UserPermissionParameterModel;

namespace Aklion.Crm.Mappers.User.UserPermission
{
    public static class UserPermissionMapper
    {
        public static PagingModel<UserPermissionModel> MapNew(this Tuple<int, List<DomainUserPermissionModel>> tuple, int? page, int? size)
        {
            return new PagingModel<UserPermissionModel>(tuple.Item2.MapListNew<UserPermissionModel>(), tuple.Item1, page, size);
        }

        public static DomainUserPermissionModel MapNew(this UserPermissionModel model, int storeId)
        {
            var result = model.MapNew<DomainUserPermissionModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserPermissionModel MapFrom(this DomainUserPermissionModel domainModel, UserPermissionModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainUserPermissionParameterModel MapNew(this UserPermissionParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainUserPermissionParameterModel>();
            result.StoreId = storeId;

            return result;
        }
    }
}