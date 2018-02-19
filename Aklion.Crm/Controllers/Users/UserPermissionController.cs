using System.Collections.Generic;
using Aklion.Crm.Business.Permission;
using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("Permissions")]
    public class UserPermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public UserPermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("GetForSelect")]
        public Dictionary<string, Permission> GetList()
        {
            return _permissionService.GetForUserWithNames();
        }
    }
}