using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Mappers.Administration.AuditLog;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.AuditLog;
using Aklion.Infrastructure.AuditLogger;
using Aklion.Infrastructure.AuditLogger.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class AuditLogsController : BaseController
    {
        private readonly IAuditLogger _logger;

        public AuditLogsController(IAuditLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PagingModel<AuditLogModel>> GetList(AuditLogParameterModel model)
        {
            var result = await _logger.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Dictionary<string, AuditLogActionType> GetActionTypes()
        {
            return _logger.GetActionTypes();
        }

        [HttpGet]
        public Dictionary<string, AuditLogObjectType> GetObjectTypes()
        {
            return _logger.GetObjectTypes();
        }
    }
}