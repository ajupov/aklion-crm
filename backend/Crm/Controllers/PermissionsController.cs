using System.Collections.Generic;
using Crm.Attributes;
using Crm.Business.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Permissions")]
    public class PermissionsController : BaseController
    {
        private readonly IPermissionService _service;

        public PermissionsController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetForSelect")]
        public Dictionary<string, Enums.Permission> GetSelect()
        {
            return _service.GetForUserWithNames();
        }
    }
}