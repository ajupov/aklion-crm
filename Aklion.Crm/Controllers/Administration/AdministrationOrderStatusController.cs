using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderStatus;
using Aklion.Crm.Mappers.Administration.OrderStatus;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderStatuses")]
    public class AdministrationOrderStatusController : BaseController
    {
        private readonly IOrderStatusDao _orderStatusDao;

        public AdministrationOrderStatusController(IOrderStatusDao orderStatusDao)
        {
            _orderStatusDao = orderStatusDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderStatusModel>> GetList(OrderStatusParameterModel model)
        {
            var result = await _orderStatusDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _orderStatusDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(OrderStatusModel model)
        {
            return _orderStatusDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderStatusModel model)
        {
            var result = await _orderStatusDao.GetAsync(model.Id).ConfigureAwait(false);

            await _orderStatusDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _orderStatusDao.DeleteAsync(id);
        }
    }
}