using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Mappers.Administration.AuditLog;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.AuditLog;
using Aklion.Infrastructure.AuditLogger;
using Aklion.Infrastructure.AuditLogger.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.AuditLogs
{
    [AjaxErrorHandle]
    [Route("Administration/AuditLogs")]
    public class AdministrationAuditLogController : BaseController
    {
        private readonly IAuditLogger _service;

        public AdministrationAuditLogController(IAuditLogger service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<PagingModel<AuditLogModel>> GetList(AuditLogParameterModel model)
        {
            var result = await _service.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
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