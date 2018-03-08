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
    [AjaxErrorHandle]
    public class ProductAttributesController : BaseController
    {
        private readonly IProductAttributeDao _dao;

        public ProductAttributesController(IProductAttributeDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductAttributeModel>> GetList(ProductAttributeParameterModel model)
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
        public Task Create(ProductAttributeModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ProductAttributeModel model)
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