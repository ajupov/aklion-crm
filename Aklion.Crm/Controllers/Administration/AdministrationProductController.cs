using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.Administration.Product;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Product;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Products")]
    public class AdministrationProductController : Controller
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
            var result = await _productDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            var result = await _productDao.GetForAutocompleteByNamePattern(pattern, storeId).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(ProductModel model)
        {
            var store = model.Map();

            await _productDao.Create(store).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(ProductModel model)
        {
            var store = await _productDao.Get(model.Id).ConfigureAwait(false);
            if (store == null)
            {
                return false;
            }

            model.Map(store);

            await _productDao.Update(store).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _productDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}