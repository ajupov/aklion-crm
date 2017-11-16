using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Attribute;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Dao.ProductAttribute;
using Aklion.Crm.Mappers.Administration.ProductAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductAttributes")]
    public class AdministrationProductAttributeController : Controller
    {
        private readonly IProductAttributeDao _productAttributeDao;
        private readonly IAttributeDao _attributeDao;
        private readonly IProductDao _productDao;

        public AdministrationProductAttributeController(
            IProductAttributeDao productAttributeDao,
            IAttributeDao attributeDao,
            IProductDao productDao)
        {
            _productAttributeDao = productAttributeDao;
            _attributeDao = attributeDao;
            _productDao = productDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductAttributeModel>> GetList(ProductAttributeParameterModel model)
        {
            var result = await _productAttributeDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(ProductAttributeModel model)
        {
            var attribute = await _attributeDao.Get(model.AttributeId).ConfigureAwait(false);
            if (attribute.StoreId != model.StoreId)
            {
                return false;
            }

            var product = await _productDao.Get(model.ProductId).ConfigureAwait(false);
            if (product.StoreId != model.StoreId)
            {
                return false;
            }

            var productAttribute = model.Map();

            await _productAttributeDao.Create(productAttribute).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(ProductAttributeModel model)
        {
            var attribute = await _attributeDao.Get(model.AttributeId).ConfigureAwait(false);
            if (attribute.StoreId != model.StoreId)
            {
                return false;
            }

            var product = await _productDao.Get(model.ProductId).ConfigureAwait(false);
            if (product.StoreId != model.StoreId)
            {
                return false;
            }

            var productAttribute = await _productAttributeDao.Get(model.Id).ConfigureAwait(false);
            if (productAttribute == null)
            {
                return false;
            }

            model.Map(productAttribute);

            await _productAttributeDao.Update(productAttribute).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _productAttributeDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}