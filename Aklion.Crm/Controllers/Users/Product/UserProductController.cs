using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Mappers.User.Product;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Product;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("Products")]
    public class UserProductController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IProductDao _productDao;

        public UserProductController(
            IAuditLogger auditLogService,
            IProductDao productDao)
        {
            _auditLogService = auditLogService;
            _productDao = productDao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/User/Product/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductModel>> GetList(ProductParameterModel model)
        {
            var result = await _productDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern)
        {
            return _productDao.GetForAutocompleteAsync(pattern.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ProductModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

            newModel.Id = await _productDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ProductModel model)
        {
            var oldModel = await _productDao.GetAsync(model.Id).ConfigureAwait(false);
            if (oldModel.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _productDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _productDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _productDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}