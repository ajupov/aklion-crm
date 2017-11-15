using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Category;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Dao.ProductCategory;
using Aklion.Crm.Mappers.Administration.ProductCategory;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductCategories")]
    public class AdministrationProductCategoryController : Controller
    {
        private readonly IProductCategoryDao _productCategoryDao;
        private readonly ICategoryDao _categoryDao;
        private readonly IProductDao _productDao;

        public AdministrationProductCategoryController(
            IProductCategoryDao productCategoryDao,
            ICategoryDao categoryDao,
            IProductDao productDao)
        {
            _productCategoryDao = productCategoryDao;
            _categoryDao = categoryDao;
            _productDao = productDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductCategoryModel>> GetList(ProductCategoryParameterModel model)
        {
            var result = await _productCategoryDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(ProductCategoryModel model)
        {
            var category = await _categoryDao.Get(model.CategoryId).ConfigureAwait(false);
            if (category.StoreId != model.StoreId)
            {
                return false;
            }

            var product = await _productDao.Get(model.ProductId).ConfigureAwait(false);
            if (product.StoreId != model.StoreId)
            {
                return false;
            }

            var productCategory = model.Map();

            await _productCategoryDao.Create(productCategory).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(ProductCategoryModel model)
        {
            var category = await _categoryDao.Get(model.CategoryId).ConfigureAwait(false);
            if (category.StoreId != model.StoreId)
            {
                return false;
            }

            var product = await _productDao.Get(model.ProductId).ConfigureAwait(false);
            if (product.StoreId != model.StoreId)
            {
                return false;
            }

            var productCategory = await _productCategoryDao.Get(model.Id).ConfigureAwait(false);
            if (productCategory == null)
            {
                return false;
            }

            model.Map(productCategory);

            await _productCategoryDao.Update(productCategory).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _productCategoryDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}