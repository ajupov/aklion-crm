using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderItem;
using Aklion.Crm.Mappers.Administration.OrderItem;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderItem;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderItems")]
    public class AdministrationOrderItemController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderItemDao _orderItemDao;

        public AdministrationOrderItemController(
            IAuditLogService auditLogService,
            IOrderItemDao orderItemDao)
        {
            _auditLogService = auditLogService;
            _orderItemDao = orderItemDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderItemModel>> GetList(OrderItemParameterModel model)
        {
            var result = await _orderItemDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderItemModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _orderItemDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderItemModel model)
        {
            var oldModel = await _orderItemDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _orderItemDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _orderItemDao.GetAsync(id).ConfigureAwait(false);

            await _orderItemDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}