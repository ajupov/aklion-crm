using System.Collections.Generic;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class PermissionsController : BaseController
    {
        private readonly IPermissionService _service;

        public PermissionsController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public Dictionary<string, Enums.Permission> GetSelect()
        {
            return _service.GetForAdminWithNames();
        }
    }
}