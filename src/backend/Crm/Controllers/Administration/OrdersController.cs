using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.Order;
using Crm.Mappers.Administration.Order;
using Crm.Models;
using Crm.Models.Administration.Order;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class OrdersController : BaseController
    {
        private readonly IOrderDao _dao;

        public OrdersController(IOrderDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<Dictionary<int, int>> GetAutocomplete(int pattern, int storeId)
        {
            var result = await _dao.GetAutocompleteAsync(pattern, storeId).ConfigureAwait(false);
            return result.MapNew();
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