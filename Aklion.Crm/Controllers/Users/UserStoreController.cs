using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Business.Store;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers.User.Store;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("Stores")]
    public class UserStoreController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IStoreService _storeService;
        private readonly IStoreDao _storeDao;

        public UserStoreController(
            IAuditLogger auditLogService,
            IStoreService storeService,
            IStoreDao storeDao)
        {
            _auditLogService = auditLogService;
            _storeService = storeService;
            _storeDao = storeDao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/User/Store/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _storeDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern)
        {
            return _storeDao.GetForAutocompleteAsync(pattern.MapNew());
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(StoreModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

            newModel.Id = await _storeDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(StoreModel model)
        {
            var oldModel = await _storeDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _storeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _storeDao.GetAsync(id).ConfigureAwait(false);

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _storeDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }

        [HttpPost]
        [Route("GenerateApiSecret")]
        [AjaxErrorHandle]
        public Task<string> GenerateApiSecret(int id)
        {
            return _storeService.GenerateApiSecretAsync(UserContext.UserId, UserContext.StoreId, id);
        }
    }
}