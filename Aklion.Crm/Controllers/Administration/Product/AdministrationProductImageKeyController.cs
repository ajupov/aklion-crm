using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductImageKey;
using Aklion.Crm.Mappers.Administration.ProductImageKey;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductImageKey;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Product
{
    [AjaxErrorHandle]
    [Route("Administration/ProductImageKeys")]
    public class AdministrationProductImageKeyController : BaseController
    {
        private readonly IProductImageKeyDao _dao;

        public AdministrationProductImageKeyController(IProductImageKeyDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductImageKeyModel>> GetList(ProductImageKeyParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public async Task<Dictionary<string, int>> GetForSelect(int storeId)
        {
            var result = await _dao.GetForSelectAsync(storeId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        public Task Create(ProductImageKeyModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ProductImageKeyModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.GetAsync(id);
        }
    }
}