using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderSource;
using Aklion.Crm.Mappers.Administration.OrderSource;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderSource;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Order
{
    [AjaxErrorHandle]
    [Route("Administration/OrderSources")]
    public class AdministrationOrderSourceController : BaseController
    {
        private readonly IOrderSourceDao _dao;

        public AdministrationOrderSourceController(IOrderSourceDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetForSelect(int storeId)
        {
            return _dao.GetSelectAsync(storeId.MapNew());
        }

        [HttpPost]
        public Task Create(OrderSourceModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(OrderSourceModel model)
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