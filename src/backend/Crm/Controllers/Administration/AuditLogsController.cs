using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.AuditLog;
using Crm.Enums;
using Crm.Mappers.Administration.AuditLog;
using Crm.Models;
using Crm.Models.Administration.AuditLog;
using Infrastructure.AuditLogger;
using Infrastructure.AuditLogger.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class AuditLogsController : BaseController
    {
        private readonly IAuditLogger _logger;
        private readonly IAuditLogService _service;

        public AuditLogsController(IAuditLogger logger, IAuditLogService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<List<AuditLogModel>> GetList(AuditLogParameterModel model)
        {
            var result = await _logger.GetListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpGet]
        public Dictionary<string, AuditLogActionType> GetActionTypes()
        {
            return _service.GetActionTypes();
        }

        [HttpGet]
        public Dictionary<string, AuditLogObjectType> GetObjectTypes()
        {
            return _service.GetObjectTypes();
        }
    }
}