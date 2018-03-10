using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.AuditLog;
using Crm.Enums;
using Crm.Mappers.Administration.AuditLog;
using Crm.Models.Administration.AuditLog;
using Infrastructure.AuditLogger;
using Infrastructure.AuditLogger.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/AuditLogs")]
    public class AdministrationAuditLogsController : BaseController
    {
        private readonly IAuditLogger _logger;
        private readonly IAuditLogService _service;

        public AdministrationAuditLogsController(IAuditLogger logger, IAuditLogService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<List<AuditLogModel>> GetList(AuditLogParameterModel model)
        {
            var result = await _logger.GetListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpGet]
        [Route("GetActionTypes")]
        public Dictionary<string, AuditLogActionType> GetActionTypes()
        {
            return _service.GetActionTypes();
        }

        [HttpGet]
        [Route("GetObjectTypes")]
        public Dictionary<string, AuditLogObjectType> GetObjectTypes()
        {
            return _service.GetObjectTypes();
        }
    }
}