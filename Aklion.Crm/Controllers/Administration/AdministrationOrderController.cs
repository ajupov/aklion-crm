using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Order;
using Aklion.Crm.Mappers.Administration.Order;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Order;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Orders")]
    public class AdministrationOrderController : BaseController
    {
        private readonly IOrderDao _orderDao;

        public AdministrationOrderController(IOrderDao orderDao)
        {
            _orderDao = orderDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Order/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderModel>> GetList(OrderParameterModel model)
        {
            var result = await _orderDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(OrderModel model)
        {
            return _orderDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderModel model)
        {
            var result = await _orderDao.GetAsync(model.Id).ConfigureAwait(false);

            await _orderDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _orderDao.DeleteAsync(id);
        }
    }
}