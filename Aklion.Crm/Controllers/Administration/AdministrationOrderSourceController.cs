using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderSource;
using Aklion.Crm.Mappers.Administration.OrderSource;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderSource;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderSources")]
    public class AdministrationOrderSourceController : BaseController
    {
        private readonly IOrderSourceDao _orderSourceDao;

        public AdministrationOrderSourceController(IOrderSourceDao orderSourceDao)
        {
            _orderSourceDao = orderSourceDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var result = await _orderSourceDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _orderSourceDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(OrderSourceModel model)
        {
            return _orderSourceDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderSourceModel model)
        {
            var result = await _orderSourceDao.GetAsync(model.Id).ConfigureAwait(false);

            await _orderSourceDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _orderSourceDao.DeleteAsync(id);
        }
    }
}