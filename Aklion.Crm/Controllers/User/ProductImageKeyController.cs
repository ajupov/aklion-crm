using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ProductImageKey;
using Aklion.Crm.Mappers.User.ProductImageKey;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductImageKey;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("ProductImageKeys")]
    public class ProductImageKeyController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IProductImageKeyDao _productImageKeyDao;

        public ProductImageKeyController(
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
            var result = await _productImageKeyDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }


        [HttpGet]
        [Route("GetForSelect")]
        public async Task<Dictionary<string, int>> GetForSelect()
        {
            var result = await _productImageKeyDao.GetForSelectAsync(UserContext.StoreId.MapNew()).ConfigureAwait(false);

            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductImageKeyModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _productImageKeyDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _productImageKeyDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _productImageKeyDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}