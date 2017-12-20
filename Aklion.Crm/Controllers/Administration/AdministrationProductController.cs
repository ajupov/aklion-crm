using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Mappers.Administration.Product;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Product;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Products")]
    public class AdministrationProductController : BaseController
    {
        private readonly IProductDao _productDao;

        public AdministrationProductController(IProductDao productDao)
        {
            _productDao = productDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Product/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductModel>> GetList(ProductParameterModel model)
        {
            var result = await _productDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _productDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ProductModel model)
        {
            return _productDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductModel model)
        {
            var result = await _productDao.GetAsync(model.Id).ConfigureAwait(false);

            await _productDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _productDao.DeleteAsync(id);
        }
    }
}