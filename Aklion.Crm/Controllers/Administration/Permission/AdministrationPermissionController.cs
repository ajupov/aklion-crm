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
        [Route("GetSelect")]
        public Dictionary<string, Enums.Permission> GetSelect()
        {
            return _service.GetForAdminWithNames();
        }
    }
}