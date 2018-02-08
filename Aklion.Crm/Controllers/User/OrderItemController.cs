using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderItem;
using Aklion.Crm.Mappers.User.OrderItem;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderItem;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("OrderItems")]
    public class OrderItemController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderItemDao _orderItemDao;

        public OrderItemController(
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
            var result = await _orderItemDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderItemModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _orderItemDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _orderItemDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _orderItemDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}