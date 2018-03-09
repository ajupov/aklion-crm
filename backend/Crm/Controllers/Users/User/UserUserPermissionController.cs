using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.UserPermission;
using Crm.Mappers.User.UserPermission;
using Crm.Models;
using Crm.Models.User.UserPermission;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Users.User
{
    [AjaxErrorHandle]
    [Route("UserPermissions")]
    public class UserUserPermissionController : BaseController
    {
        private readonly IUserPermissionService _service;

        public UserUserPermissionController(IUserPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<PagingModel<UserPermissionExistModel>> GetList(UserPermissionParameterModel model)
        {
            if(model.UserId <= 0)
            {
                return new PagingModel<UserPermissionExistModel>(null, 0, 0, 0);
            }

            var result = await _service.GetForUserAsync(model.UserId, UserContext.StoreId).ConfigureAwait(false);
            return result.MapNew(model.UserId);
        }

        [HttpPost]
        public Task Switch(int userId, Enums.Permission permission)
        {
            return _service.SwitchAsync(userId, UserContext.StoreId, permission);
        }
    }
}