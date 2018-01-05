using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
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
        private readonly IAuditLogService _auditLogService;
        private readonly IProductStatusDao _productStatusDao;

        public AdministrationProductStatusController(
            IAuditLogService auditLogService,
            IProductStatusDao productStatusDao)
        {
            _auditLogService = auditLogService;
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
        public async Task Create(ProductStatusModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _productStatusDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductStatusModel model)
        {
            var oldModel = await _productStatusDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _productStatusDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _productStatusDao.GetAsync(id).ConfigureAwait(false);

            await _productStatusDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}