using System.Collections.Generic;
using Aklion.Crm.Business.Permission;
using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Permissions")]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("GetForSelect")]
        public Dictionary<string, Permission> GetList()
        {
            return _permissionService.GetForAdminWithNames();
        }
    }
}