using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Order;
using Aklion.Crm.Mappers.Administration.Order;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Order;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Order
{
    [AjaxErrorHandle]
    [Route("Administration/Orders")]
    public class AdministrationOrderController : BaseController
    {
        private readonly IOrderDao _dao;

        public AdministrationOrderController(IOrderDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Order/Index.cshtml");
        }

        [HttpGet]
        public async Task<PagingModel<OrderModel>> GetList(OrderParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(OrderModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(OrderModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}