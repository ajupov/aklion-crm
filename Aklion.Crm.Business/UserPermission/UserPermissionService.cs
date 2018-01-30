using System;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Crm.Business.Permission;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Domain.UserPermission;

namespace Aklion.Crm.Business.UserPermission
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserPermissionDao _userPermissionDao;

        public UserPermissionService(
            IPermissionService permissionService,
            IUserPermissionDao userPermissionDao)
        {
            _userPermissionDao = userPermissionDao;
            _permissionService = permissionService;
        }

        public Task CreateForRegisteredUserAsync(int userId, int storeId)
        {
            var permissions = _permissionService.GetForUser();
            var now = DateTime.Now;

            var models = permissions.Select(p => new UserPermissionModel
            {
                UserId = userId,
                StoreId = storeId,
                CreateDate = now,
                ModifyDate = null,
                Permission = p
            }).ToList();

            return _userPermissionDao.CreateListAsync(models);
        }
    }
}