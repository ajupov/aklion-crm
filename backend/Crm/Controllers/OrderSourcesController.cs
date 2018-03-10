using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderSource;
using Crm.Exceptions;
using Crm.Mappers.User.OrderSource;
using Crm.Models;
using Crm.Models.User.OrderSource;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("OrderSources")]
    public class OrderSourcesController : BaseController
    {
        private readonly IOrderSourceDao _dao;

        public OrderSourcesController(IOrderSourceDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            var result = await _dao.GetSelectAsync(UserContext.StoreId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderSourceModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
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
        [Route("Delete")]
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