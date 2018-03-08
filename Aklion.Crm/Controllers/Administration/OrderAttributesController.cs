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
    [AjaxErrorHandle]
    public class OrderAttributesController : BaseController
    {
        private readonly IOrderAttributeDao _dao;

        public OrderAttributesController(IOrderAttributeDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderAttributeModel>> GetList(OrderAttributeParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern, int storeId)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        public Task Create(OrderAttributeModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(OrderAttributeModel model)
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