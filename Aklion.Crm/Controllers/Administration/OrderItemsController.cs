using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderItem;
using Aklion.Crm.Mappers.Administration.OrderItem;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderItem;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class OrderItemsController : BaseController
    {
        private readonly IOrderItemDao _dao;

        public OrderItemsController(IOrderItemDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderItemModel>> GetList(OrderItemParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(OrderItemModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(OrderItemModel model)
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