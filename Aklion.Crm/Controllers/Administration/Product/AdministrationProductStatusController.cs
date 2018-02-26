using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductStatus;
using Aklion.Crm.Mappers.Administration.ProductStatus;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductStatus;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Product
{
    [AjaxErrorHandle]
    [Route("Administration/ProductStatuses")]
    public class AdministrationProductStatusController : BaseController
    {
        private readonly IProductStatusDao _dao;

        public AdministrationProductStatusController(IProductStatusDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductStatusModel>> GetList(ProductStatusParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public async Task<Dictionary<string, int>> GetForSelect(int storeId)
        {
            var result = await _dao.GetSelectAsync(storeId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        public Task Create(ProductStatusModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ProductStatusModel model)
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