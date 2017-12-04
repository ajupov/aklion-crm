using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Tag;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Dao.ProductTag;
using Aklion.Crm.Mappers.Administration.ProductTag;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductTag;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductTags")]
    public class AdministrationProductTagController : BaseController
    {
        private readonly IProductTagDao _productTagDao;
        private readonly ITagDao _tagDao;
        private readonly IProductDao _productDao;

        public AdministrationProductTagController(
            IProductTagDao productTagDao,
            ITagDao tagDao,
            IProductDao productDao)
        {
            _productTagDao = productTagDao;
            _tagDao = tagDao;
            _productDao = productDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductTagModel>> GetList(ProductTagParameterModel model)
        {
            var result = await _productTagDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(ProductTagModel model)
        {
            var tag = await _tagDao.Get(model.TagId).ConfigureAwait(false);
            if (tag.StoreId != model.StoreId)
            {
                return false;
            }

            var product = await _productDao.Get(model.ProductId).ConfigureAwait(false);
            if (product.StoreId != model.StoreId)
            {
                return false;
            }

            var productTag = model.Map();

            await _productTagDao.Create(productTag).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(ProductTagModel model)
        {
            var tag = await _tagDao.Get(model.TagId).ConfigureAwait(false);
            if (tag.StoreId != model.StoreId)
            {
                return false;
            }

            var product = await _productDao.Get(model.ProductId).ConfigureAwait(false);
            if (product.StoreId != model.StoreId)
            {
                return false;
            }

            var productTag = await _productTagDao.Get(model.Id).ConfigureAwait(false);
            if (productTag == null)
            {
                return false;
            }

            model.Map(productTag);

            await _productTagDao.Update(productTag).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _productTagDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}