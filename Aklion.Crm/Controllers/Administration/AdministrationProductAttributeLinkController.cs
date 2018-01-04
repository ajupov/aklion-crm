using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductAttributeLink;
using Aklion.Crm.Mappers.Administration.ProductAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductAttributeLinks")]
    public class AdministrationProductAttributeLinkController : BaseController
    {
        private readonly IProductAttributeLinkDao _productAttributeLinkDao;

        public AdministrationProductAttributeLinkController(IProductAttributeLinkDao productAttributeLinkDao)
        {
            _productAttributeLinkDao = productAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductAttributeLinkModel>> GetList(ProductAttributeLinkParameterModel model)
        {
            var result = await _productAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ProductAttributeLinkModel model)
        {
            return _productAttributeLinkDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductAttributeLinkModel model)
        {
            var result = await _productAttributeLinkDao.GetAsync(model.Id).ConfigureAwait(false);

            await _productAttributeLinkDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _productAttributeLinkDao.DeleteAsync(id);
        }
    }
}