using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ClientAttributeLink;
using Aklion.Crm.Mappers.Administration.ClientAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("ClientAttributeLinks")]
    public class ClientAttributeLinkController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IClientAttributeLinkDao _clientAttributeLinkDao;

        public ClientAttributeLinkController(
            IAuditLogService auditLogService,
            IClientAttributeLinkDao clientAttributeLinkDao)
        {
            _auditLogService = auditLogService;
            _clientAttributeLinkDao = clientAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeLinkModel>> GetList(ClientAttributeLinkParameterModel model)
        {
            var result = await _clientAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ClientAttributeLinkModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _clientAttributeLinkDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ClientAttributeLinkModel model)
        {
            var oldModel = await _clientAttributeLinkDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _clientAttributeLinkDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _clientAttributeLinkDao.GetAsync(id).ConfigureAwait(false);

            await _clientAttributeLinkDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}