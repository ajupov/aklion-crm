using System.Threading.Tasks;
using Aklion.Crm.Attributes;
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
        private readonly IOrderItemDao _orderItemDao;

        public AdministrationOrderItemController(IOrderItemDao orderItemDao)
        {
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
        public Task Create(OrderItemModel model)
        {
            return _orderItemDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderItemModel model)
        {
            var result = await _orderItemDao.GetAsync(model.Id).ConfigureAwait(false);

            await _orderItemDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _orderItemDao.DeleteAsync(id);
        }
    }
}