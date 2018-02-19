using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductAttribute;
using Aklion.Crm.Mappers.Administration.ProductAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.AdministrationControllers
{
    [Route("Administration/ProductAttributes")]
    public class AdministrationProductAttributeController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IProductAttributeDao _productAttributeDao;

        public AdministrationProductAttributeController(
            IAuditLogger auditLogService,
            IProductAttributeDao productAttributeDao)
        {
            _auditLogService = auditLogService;
            _productAttributeDao = productAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductAttributeModel>> GetList(ProductAttributeParameterModel model)
        {
            var result = await _productAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByDescriptionPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByDescriptionPattern(string pattern, int storeId = 0)
        {
            return _productAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductAttributeModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _productAttributeDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductAttributeModel model)
        {
            var oldModel = await _productAttributeDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _productAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _productAttributeDao.GetAsync(id).ConfigureAwait(false);

            await _productAttributeDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}