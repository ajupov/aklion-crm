using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ProductAttributeLink;
using Aklion.Crm.Mappers.Administration.ProductAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("ProductAttributeLinks")]
    public class ProductAttributeLinkController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IProductAttributeLinkDao _productAttributeLinkDao;

        public ProductAttributeLinkController(
            IAuditLogService auditLogService,
            IProductAttributeLinkDao productAttributeLinkDao)
        {
            _auditLogService = auditLogService;
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
        public async Task Create(ProductAttributeLinkModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _productAttributeLinkDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductAttributeLinkModel model)
        {
            var oldModel = await _productAttributeLinkDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _productAttributeLinkDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _productAttributeLinkDao.GetAsync(id).ConfigureAwait(false);

            await _productAttributeLinkDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}