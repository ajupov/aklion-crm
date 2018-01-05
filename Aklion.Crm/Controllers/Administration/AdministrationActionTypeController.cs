using System.Collections.Generic;
using Aklion.Crm.Business.ActionType;
using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ActionTypes")]
    public class AdministrationActionTypeController : BaseController
    {
        private readonly IActionTypeService _actionTypeService;

        public AdministrationActionTypeController(IActionTypeService actionTypeService)
        {
            _actionTypeService = actionTypeService;
        }

        [HttpGet]
        [Route("GetForSelect")]
        public Dictionary<string, AuditLogActionType> GetList()
        {
            return _actionTypeService.Get();
        }
    }
}