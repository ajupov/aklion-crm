using System.Collections.Generic;
using Aklion.Crm.Business.Permission;
using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Permissions")]
    public class AdministrationPermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public AdministrationPermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Permission/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public Dictionary<string, Permission> GetList()
        {
            return _permissionService.GetForAdmin();
        }
    }
}