using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderAttributeLink;
using Aklion.Crm.Mappers.Administration.OrderAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderAttributeLinks")]
    public class AdministrationOrderAttributeLinkController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderAttributeLinkDao _clientAttributeLinkDao;

        public AdministrationOrderAttributeLinkController(
            IAuditLogService auditLogService,
            IOrderAttributeLinkDao clientAttributeLinkDao)
        {
            _auditLogService = auditLogService;
            _clientAttributeLinkDao = clientAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeLinkModel>> GetList(OrderAttributeLinkParameterModel model)
        {
            var result = await _clientAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderAttributeLinkModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _clientAttributeLinkDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderAttributeLinkModel model)
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