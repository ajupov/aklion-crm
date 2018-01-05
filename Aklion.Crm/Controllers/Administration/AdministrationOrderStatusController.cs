using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderStatus;
using Aklion.Crm.Mappers.Administration.OrderStatus;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderStatuses")]
    public class AdministrationOrderStatusController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderStatusDao _orderStatusDao;

        public AdministrationOrderStatusController(
            IAuditLogService auditLogService,
            IOrderStatusDao orderStatusDao)
        {
            _auditLogService = auditLogService;
            _orderStatusDao = orderStatusDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderStatusModel>> GetList(OrderStatusParameterModel model)
        {
            var result = await _orderStatusDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForSelect")]
        public async Task<Dictionary<string, int>> GetList(int storeId = 0)
        {
            var result = await _orderStatusDao.GetForSelectAsync(storeId.MapNew()).ConfigureAwait(false);

            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderStatusModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _orderStatusDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderStatusModel model)
        {
            var oldModel = await _orderStatusDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _orderStatusDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _orderStatusDao.GetAsync(id).ConfigureAwait(false);

            await _orderStatusDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}