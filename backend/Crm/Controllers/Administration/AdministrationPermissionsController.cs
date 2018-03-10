using System.Collections.Generic;
using Crm.Attributes;
using Crm.Business.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/Permissions")]
    public class AdministrationPermissionsController : BaseController
    {
        private readonly IPermissionService _service;

        public AdministrationPermissionsController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetSelect")]
        public Dictionary<string, Enums.Permission> GetSelect()
        {
            return _service.GetForAdminWithNames();
        }
    }
}