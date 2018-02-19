using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderStatus;
using Aklion.Crm.Mappers.Administration.OrderStatus;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Order
{
    [AjaxErrorHandle]
    [Route("Administration/OrderStatuses")]
    public class AdministrationOrderStatusController : BaseController
    {
        private readonly IOrderStatusDao _dao;

        public AdministrationOrderStatusController(IOrderStatusDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderStatusModel>> GetList(OrderStatusParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public async Task<Dictionary<string, int>> GetForSelect(int storeId)
        {
            var result = await _dao.GetSelectAsync(storeId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        public Task Create(OrderStatusModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(OrderStatusModel model)
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