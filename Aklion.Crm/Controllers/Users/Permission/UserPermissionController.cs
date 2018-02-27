using System.Collections.Generic;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Users.Permission
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