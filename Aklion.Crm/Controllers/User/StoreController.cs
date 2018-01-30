using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Business.Store;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers.Administration.Store;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Stores")]
    public class StoreController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IStoreService _storeService;
        private readonly IStoreDao _storeDao;

        public StoreController(
            IAuditLogService auditLogService,
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
            return View("~/Views/Administration/Store/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _storeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

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
            var newModel = model.MapNew();

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

            var newModel = oldModel.MapFrom(model);

            await _storeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _storeDao.GetAsync(id).ConfigureAwait(false);

            await _storeDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
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