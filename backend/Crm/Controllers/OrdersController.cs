using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.Order;
using Crm.Exceptions;
using Crm.Mappers.User.Order;
using Crm.Models;
using Crm.Models.User.Order;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Orders")]
    public class OrdersController : BaseController
    {
        private readonly IOrderDao _dao;

        public OrdersController(IOrderDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Order/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderModel>> GetList(OrderParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            result.IsDeleted = !result.IsDeleted;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}