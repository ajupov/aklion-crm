using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderItem;
using Crm.Exceptions;
using Crm.Mappers.User.OrderItem;
using Crm.Models;
using Crm.Models.User.OrderItem;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("OrderItems")]
    public class OrderItemsController : BaseController
    {
        private readonly IOrderItemDao _dao;

        public OrderItemsController(IOrderItemDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderItemModel>> GetList(OrderItemParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderItemModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderItemModel model)
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