using System.Collections.Generic;
using Aklion.Crm.Models.User.UserPermission;
using Aklion.Infrastructure.DisplayName;
using Aklion.Infrastructure.Mapper;
using Aklion.Crm.Models;
using DomainUserPermissionExistModel = Aklion.Crm.Domain.UserPermission.UserPermissionExistModel;

namespace Aklion.Crm.Mappers.User.UserPermission
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