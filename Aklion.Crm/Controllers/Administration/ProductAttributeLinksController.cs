using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductAttributeLink;
using Aklion.Crm.Mappers.Administration.ProductAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class ProductAttributeLinksController : BaseController
    {
        private readonly IProductAttributeLinkDao _dao;

        public ProductAttributeLinksController(IProductAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductAttributeLinkModel>> GetList(ProductAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(ProductAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ProductAttributeLinkModel model)
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