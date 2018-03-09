using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderSource;
using Crm.Exceptions;
using Crm.Mappers.User.OrderSource;
using Crm.Models;
using Crm.Models.User.OrderSource;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Users.Order
{
    [AjaxErrorHandle]
    [Route("OrderSources")]
    public class UserOrderSourceController : BaseController
    {
        private readonly IOrderSourceDao _dao;

        public UserOrderSourceController(IOrderSourceDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public async Task<Dictionary<string, int>> GetForSelect()
        {
            var result = await _dao.GetSelectAsync(UserContext.StoreId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        public Task Create(OrderSourceModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        public async Task Update(OrderSourceModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}