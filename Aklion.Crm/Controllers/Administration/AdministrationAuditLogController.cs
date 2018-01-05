using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.AuditLog;
using Aklion.Crm.Enums;
using Aklion.Crm.Mappers.Administration.AuditLog;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.AuditLog;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/AuditLogs")]
    public class AdministrationAuditLogController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IAuditLogDao _auditLogDao;

        public AdministrationAuditLogController(
            IAuditLogService auditLogService,
            IAuditLogDao auditLogDao)
        {
            _auditLogService = auditLogService;
            _auditLogDao = auditLogDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<AuditLogModel>> GetList(AuditLogParameterModel model)
        {
            var result = await _auditLogDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetActionTypesForSelect")]
        public Dictionary<string, AuditLogActionType> GetActionTypesForSelect()
        {
            return _auditLogService.GetActionTypes();
        }

        [HttpGet]
        [Route("GetObjectTypesForSelect")]
        public Dictionary<string, AuditLogObjectType> GetObjectTypesForSelect()
        {
            return _auditLogService.GetObjectTypes();
        }
    }
}