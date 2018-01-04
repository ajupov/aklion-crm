using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductStatus;
using Aklion.Crm.Mappers.Administration.ProductStatus;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductStatus;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductStatuses")]
    public class AdministrationProductStatusController : BaseController
    {
        private readonly IProductStatusDao _productStatusDao;

        public AdministrationProductStatusController(IProductStatusDao productStatusDao)
        {
            _productStatusDao = productStatusDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductStatusModel>> GetList(ProductStatusParameterModel model)
        {
            var result = await _productStatusDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForSelect")]
        public async Task<Dictionary<string, int>> GetList(int storeId = 0)
        {
            var result = await _productStatusDao.GetForSelectAsync(storeId.MapNew()).ConfigureAwait(false);

            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ProductStatusModel model)
        {
            return _productStatusDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductStatusModel model)
        {
            var result = await _productStatusDao.GetAsync(model.Id).ConfigureAwait(false);

            await _productStatusDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _productStatusDao.DeleteAsync(id);
        }
    }
}