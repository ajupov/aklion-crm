using System.Collections.Generic;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Permission
{
    [AjaxErrorHandle]
    [Route("Administration/Permissions")]
    public class AdministrationPermissionController : BaseController
    {
        private readonly IPermissionService _service;

        public AdministrationPermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public Dictionary<string, Enums.Permission> GetForSelect()
        {
            return _service.GetForAdminWithNames();
        }
    }
}