using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.Order;
using Aklion.Crm.Mappers.User.Order;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Order;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("Orders")]
    public class OrderController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderDao _orderDao;

        public OrderController(
            IAuditLogService auditLogService,
            IOrderDao orderDao)
        {
            _auditLogService = auditLogService;
            _orderDao = orderDao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/User/Order/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderModel>> GetList(OrderParameterModel model)
        {
            var result = await _orderDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

            newModel.Id = await _orderDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderModel model)
        {
            var oldModel = await _orderDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _orderDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _orderDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _orderDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}