using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductStatus;
using Aklion.Crm.Mappers.Administration.ProductStatus;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductStatus;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductStatuses")]
    public class AdministrationProductStatusController : BaseController
    {
        private readonly IProductStatusDao _orderStatusDao;

        public AdministrationProductStatusController(IProductStatusDao orderStatusDao)
        {
            _orderStatusDao = orderStatusDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductStatusModel>> GetList(ProductStatusParameterModel model)
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
        public Task Create(ProductStatusModel model)
        {
            return _orderStatusDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductStatusModel model)
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