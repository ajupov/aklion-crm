using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ProductAttributeLink;
using Aklion.Crm.Mappers.User.ProductAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
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
            var result = await _productAttributeLinkDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductAttributeLinkModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _productAttributeLinkDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _productAttributeLinkDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _productAttributeLinkDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}