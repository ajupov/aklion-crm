using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPermission;
using Aklion.Infrastructure.Mapper;
using DomainUserPermissionModel = Aklion.Crm.Domain.UserPermission.UserPermissionModel;
using DomainUserPermissionParameterModel = Aklion.Crm.Domain.UserPermission.UserPermissionParameterModel;

namespace Aklion.Crm.Mappers.Administration.UserPermission
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