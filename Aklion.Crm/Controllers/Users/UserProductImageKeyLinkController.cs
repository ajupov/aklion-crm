using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ProductImageKeyLink;
using Aklion.Crm.Mappers.User.ProductImageKeyLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductImageKeyLink;
using Aklion.Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("ProductImageKeyLinks")]
    public class UserProductImageKeyLinkController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IProductImageKeyLinkDao _productImageKeyLinkDao;

        public UserProductImageKeyLinkController(
            IAuditLogger auditLogService,
            IProductImageKeyLinkDao productImageKeyLinkDao)
        {
            _auditLogService = auditLogService;
            _productImageKeyLinkDao = productImageKeyLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var result = await _productImageKeyLinkDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductImageKeyLinkModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

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
            var model = await _productImageKeyLinkDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _productImageKeyLinkDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}