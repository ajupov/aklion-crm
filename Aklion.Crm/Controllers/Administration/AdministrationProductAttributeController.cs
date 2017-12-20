using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductAttribute;
using Aklion.Crm.Mappers.Administration.ProductAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductAttributes")]
    public class AdministrationProductAttributeController : BaseController
    {
        private readonly IProductAttributeDao _clientAttributeDao;

        public AdministrationProductAttributeController(IProductAttributeDao clientAttributeDao)
        {
            _clientAttributeDao = clientAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductAttributeModel>> GetList(ProductAttributeParameterModel model)
        {
            var result = await _clientAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _clientAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ProductAttributeModel model)
        {
            return _clientAttributeDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductAttributeModel model)
        {
            var result = await _clientAttributeDao.GetAsync(model.Id).ConfigureAwait(false);

            await _clientAttributeDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _clientAttributeDao.DeleteAsync(id);
        }
    }
}