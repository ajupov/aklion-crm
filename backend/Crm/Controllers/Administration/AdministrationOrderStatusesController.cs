using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderStatus;
using Crm.Mappers.Administration.OrderStatus;
using Crm.Models;
using Crm.Models.Administration.OrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/OrderStatuses")]
    public class AdministrationOrderStatusesController : BaseController
    {
        private readonly IOrderStatusDao _dao;

        public AdministrationOrderStatusesController(IOrderStatusDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderStatusModel>> GetList(OrderStatusParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect(int storeId)
        {
            var result = await _dao.GetSelectAsync(storeId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderStatusModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderStatusModel model)
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