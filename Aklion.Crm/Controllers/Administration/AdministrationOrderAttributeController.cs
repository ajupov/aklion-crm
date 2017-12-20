using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderAttribute;
using Aklion.Crm.Mappers.Administration.OrderAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderAttributes")]
    public class AdministrationOrderAttributeController : BaseController
    {
        private readonly IOrderAttributeDao _orderAttributeDao;

        public AdministrationOrderAttributeController(IOrderAttributeDao orderAttributeDao)
        {
            _orderAttributeDao = orderAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeModel>> GetList(OrderAttributeParameterModel model)
        {
            var result = await _orderAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _orderAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(OrderAttributeModel model)
        {
            return _orderAttributeDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderAttributeModel model)
        {
            var result = await _orderAttributeDao.GetAsync(model.Id).ConfigureAwait(false);

            await _orderAttributeDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _orderAttributeDao.DeleteAsync(id);
        }
    }
}