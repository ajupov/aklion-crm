using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.Store;
using Crm.Business.UserPermission;
using Crm.Dao.Store;
using Crm.Mappers.User.Store;
using Crm.Models;
using Crm.Models.User.Store;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Stores")]
    public class StoresController : BaseController
    {
        private readonly IStoreService _service;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IStoreDao _dao;

        public StoresController(IStoreService service, IUserPermissionService userPermissionService, IStoreDao dao)
        {
            _service = service;
            _dao = dao;
            _userPermissionService = userPermissionService;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Store/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.UserId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(UserContext.UserId));
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(StoreModel model)
        {
            var storeId = await _dao.CreateAsync(model.MapNew()).ConfigureAwait(false);
            await _userPermissionService.CreateForRegisteredUserAsync(UserContext.UserId, storeId).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(StoreModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            result.IsDeleted = true;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GenerateApiSecret")]
        public Task<string> GenerateApiSecret(int id)
        {
            return _service.GenerateApiSecretAsync(UserContext.UserId, UserContext.StoreId, id);
        }
    }
}