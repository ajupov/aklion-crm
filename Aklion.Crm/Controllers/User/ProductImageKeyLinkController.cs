using System.IO;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ProductImageKeyLink;
using Aklion.Crm.Mappers.Administration.ProductImageKeyLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductImageKeyLink;
using Aklion.Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("ProductImageKeyLinks")]
    public class ProductImageKeyLinkController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IProductImageKeyLinkDao _productImageKeyLinkDao;

        public ProductImageKeyLinkController(
            IAuditLogService auditLogService,
            IProductImageKeyLinkDao productImageKeyLinkDao)
        {
            _auditLogService = auditLogService;
            _productImageKeyLinkDao = productImageKeyLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var result = await _productImageKeyLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductImageKeyLinkModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _productImageKeyLinkDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductImageKeyLinkModel model)
        {
            var oldModel = await _productImageKeyLinkDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _productImageKeyLinkDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [AjaxErrorHandle]
        public async Task LoadImage(ProductImageKeyLinkLoadImageModel model)
        {
            if (!model.ImageFile.FileName.IsImage())
            {
                return;
            }

            var oldModel = await _productImageKeyLinkDao.GetAsync(model.Id).ConfigureAwait(false);
            if(oldModel == null)
            {
                return;
            }

            await _productImageKeyLinkDao.SetImageAsync(model.Id, model.ImageFile.OpenReadStream()).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _productImageKeyLinkDao.GetAsync(id).ConfigureAwait(false);

            await _productImageKeyLinkDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}