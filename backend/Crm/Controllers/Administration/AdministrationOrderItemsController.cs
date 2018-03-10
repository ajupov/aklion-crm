using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderItem;
using Crm.Mappers.Administration.OrderItem;
using Crm.Models;
using Crm.Models.Administration.OrderItem;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/OrderItems")]
    public class AdministrationOrderItemsController : BaseController
    {
        private readonly IOrderItemDao _dao;

        public AdministrationOrderItemsController(IOrderItemDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderItemModel>> GetList(OrderItemParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderItemModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderItemModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}