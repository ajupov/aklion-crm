using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.Product;
using Crm.Mappers.Administration.Product;
using Crm.Models;
using Crm.Models.Administration.Product;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/Products")]
    public class AdministrationProductsController : BaseController
    {
        private readonly IProductDao _dao;

        public AdministrationProductsController(IProductDao dao)
        {
            _dao = dao;
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
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern, int storeId)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ProductModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public Task Delete(int id)
        {
            return _dao.GetAsync(id);
        }
    }
}