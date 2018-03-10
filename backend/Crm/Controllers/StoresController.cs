using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.Store;
using Crm.Business.UserPermission;
using Crm.Dao.Store;
using Crm.Dao.UserPermission;
using Crm.Exceptions;
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
        private readonly IUserPermissionDao _userPermissionDao;

        public StoresController(IStoreService service, IUserPermissionService userPermissionService, IStoreDao dao, IUserPermissionDao userPermissionDao)
        {
            _service = service;
            _userPermissionService = userPermissionService;
            _dao = dao;
            _userPermissionDao = userPermissionDao;
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
            if (!await _userPermissionDao.IsExistAsync(UserContext.UserId, model.Id).ConfigureAwait(false))
            {
                throw new NotAccessChangingException();
            }

            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            if (!await _userPermissionDao.IsExistAsync(UserContext.UserId, id).ConfigureAwait(false))
            {
                throw new NotAccessChangingException();
            }

            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            result.IsDeleted = !result.IsDeleted;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GenerateApiSecret")]
        public async Task<string> GenerateApiSecret(int id)
        {
            if (!await _userPermissionDao.IsExistAsync(UserContext.UserId, id).ConfigureAwait(false))
            {
                throw new NotAccessChangingException();
            }

            return await _service.GenerateApiSecretAsync(UserContext.UserId, UserContext.StoreId, id).ConfigureAwait(false);
        }
    }
}