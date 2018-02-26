using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Business.UserPermission;
using Aklion.Crm.Dao.UserPermission;
using Microsoft.AspNetCore.Mvc;
using Aklion.Crm.Enums;
using Aklion.Crm.Models.User.UserPermission;
using Aklion.Crm.Mappers.User.UserPermission;
using Aklion.Crm.Models;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("UserPermissions")]
    public class UserUserPermissionController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IUserPermissionDao _userPermissionDao;

        public UserUserPermissionController(
            IUserPermissionDao userPermissionDao,
            IUserPermissionService userPermissionService,
            IAuditLogger auditLogService)
        {
            _auditLogService = auditLogService;
            _userPermissionService = userPermissionService;
            _userPermissionDao = userPermissionDao;
        }

        [HttpGet]
        [Route("GetList")]
        [AjaxErrorHandle]
        public async Task<PagingModel<UserPermissionExistModel>> GetList(UserPermissionParameterModel model)
        {
            if(model.UserId <= 0)
            {
                return new PagingModel<UserPermissionExistModel>(null, 0, 0, 0);
            }

            var result = await _userPermissionService.GetForUserAsync(model.UserId, UserContext.StoreId).ConfigureAwait(false);

            return result.MapNew(model.UserId);
        }

        [HttpPost]
        [Route("Switch")]
        [AjaxErrorHandle]
        public Task Switch(int userId, Permission permission)
        {
            return _userPermissionService.SwitchAsync(userId, UserContext.StoreId, permission);
        }
    }
}