using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.UserPermission;
using Infrastructure.Mapper;
using DomainUserPermissionModel = Crm.Domain.UserPermission.UserPermissionModel;
using DomainUserPermissionParameterModel = Crm.Domain.UserPermission.UserPermissionParameterModel;

namespace Crm.Mappers.Administration.UserPermission
{
    public static class UserPermissionMapper
    {
        public static PagingModel<UserPermissionModel> MapNew(this (int TotalCount, List<DomainUserPermissionModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<UserPermissionModel>(tuple.List.MapListNew<UserPermissionModel>(), tuple.TotalCount, page, size);
        }

        public static DomainUserPermissionModel MapNew(this UserPermissionModel model)
        {
            return model.MapNew<DomainUserPermissionModel>();
        }

        public static DomainUserPermissionModel MapFrom(this DomainUserPermissionModel domainModel, UserPermissionModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainUserPermissionParameterModel MapNew(this UserPermissionParameterModel model)
        {
            return model.MapParameterNew<DomainUserPermissionParameterModel>();
        }
    }
}