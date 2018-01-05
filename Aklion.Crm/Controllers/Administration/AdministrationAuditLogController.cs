using System.Threading.Tasks;
using Aklion.Crm.Dao.AuditLog;
using Aklion.Crm.Mappers.Administration.AuditLog;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.AuditLog;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/AuditLogs")]
    public class AdministrationAuditLogController : BaseController
    {
        private readonly IAuditLogDao _auditLogDao;

        public AdministrationAuditLogController(IAuditLogDao auditLogDao)
        {
            _auditLogDao = auditLogDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<AuditLogModel>> GetList(AuditLogParameterModel model)
        {
            var result = await _auditLogDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }
    }
}