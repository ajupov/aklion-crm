using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderSource;
using Aklion.Crm.Mappers.Administration.OrderSource;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderSource;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderSources")]
    public class AdministrationOrderSourceController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderSourceDao _orderSourceDao;

        public AdministrationOrderSourceController(
            IAuditLogService auditLogService,
            IOrderSourceDao orderSourceDao)
        {
            _auditLogService = auditLogService;
            _orderSourceDao = orderSourceDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var result = await _orderSourceDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForSelect")]
        public async Task<Dictionary<string, int>> GetList(int storeId = 0)
        {
            var result = await _orderSourceDao.GetForSelectAsync(storeId.MapNew()).ConfigureAwait(false);

            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderSourceModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _orderSourceDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderSourceModel model)
        {
            var oldModel = await _orderSourceDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _orderSourceDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _orderSourceDao.GetAsync(id).ConfigureAwait(false);

            await _orderSourceDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}