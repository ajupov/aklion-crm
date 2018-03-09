using System.Collections.Generic;
using Crm.Models.User.UserPermission;
using Infrastructure.DisplayName;
using Infrastructure.Mapper;
using Crm.Models;
using DomainUserPermissionExistModel = Crm.Domain.UserPermission.UserPermissionExistModel;

namespace Crm.Mappers.User.UserPermission
{
    public static class UserPermissionMapper
    {
        public static PagingModel<UserPermissionExistModel> MapNew(this List<DomainUserPermissionExistModel> models, int userId)
        {
            return new PagingModel<UserPermissionExistModel>(models.Map(userId), models.Count, 0, models.Count);
        }

        public static List<UserPermissionExistModel> Map(this List<DomainUserPermissionExistModel> model, int userId)
        {
            var result = model.MapListNew<UserPermissionExistModel>();

            result.ForEach(r =>
            {
                r.UserId = userId;
                r.PermissionName = r.Permission.GetDisplayName();
            });

            return result;
        }
    }
}