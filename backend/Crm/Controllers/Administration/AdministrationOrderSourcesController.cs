using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderSource;
using Crm.Mappers.Administration.OrderSource;
using Crm.Models;
using Crm.Models.Administration.OrderSource;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/OrderSources")]
    public class AdministrationOrderSourcesController : BaseController
    {
        private readonly IOrderSourceDao _dao;

        public AdministrationOrderSourcesController(IOrderSourceDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public Task<Dictionary<string, int>> GetSelect(int storeId)
        {
            return _dao.GetSelectAsync(storeId.MapNew());
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderSourceModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderSourceModel model)
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