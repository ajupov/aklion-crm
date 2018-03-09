using System.Collections.Generic;
using Crm.Attributes;
using Crm.Business.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Users.Permission
{
    [AjaxErrorHandle]
    [Route("Permissions")]
    public class UserPermissionController : BaseController
    {
        private readonly IPermissionService _service;

        public UserPermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public Dictionary<string, Enums.Permission> GetForSelect()
        {
            return _service.GetForUserWithNames();
        }
    }
}