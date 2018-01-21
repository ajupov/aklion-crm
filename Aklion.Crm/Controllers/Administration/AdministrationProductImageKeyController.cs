using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ProductImageKey;
using Aklion.Crm.Mappers.Administration.ProductImageKey;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductImageKey;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ProductImageKeys")]
    public class AdministrationProductImageKeyController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IProductImageKeyDao _productImageKeyDao;

        public AdministrationProductImageKeyController(
            IAuditLogService auditLogService,
            IProductImageKeyDao productImageKeyDao)
        {
            _auditLogService = auditLogService;
            _productImageKeyDao = productImageKeyDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyModel>> GetList(ProductImageKeyParameterModel model)
        {
            var result = await _productImageKeyDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }


        [HttpGet]
        [Route("GetForSelect")]
        public async Task<Dictionary<string, int>> GetForSelect(int storeId = 0)
        {
            var result = await _productImageKeyDao.GetForSelectAsync(storeId.MapNew()).ConfigureAwait(false);

            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductImageKeyModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _productImageKeyDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductImageKeyModel model)
        {
            var oldModel = await _productImageKeyDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _productImageKeyDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _productImageKeyDao.GetAsync(id).ConfigureAwait(false);

            await _productImageKeyDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}