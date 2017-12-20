using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.Store;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers.Administration.Store;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Stores")]
    public class AdministrationStoreController : BaseController
    {
        private readonly IStoreService _storeService;
        private readonly IStoreDao _storeDao;

        public AdministrationStoreController(IStoreService storeService, IStoreDao storeDao)
        {
            _storeService = storeService;
            _storeDao = storeDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
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
        public Task Create(StoreModel model)
        {
            return _storeDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(StoreModel model)
        {
            var result = await _storeDao.GetAsync(model.Id).ConfigureAwait(false);

            await _storeDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _storeDao.DeleteAsync(id);
        }

        [HttpPost]
        [Route("GenerateApiSecret")]
        [AjaxErrorHandle]
        public Task<string> GenerateApiSecret(int id)
        {
            return _storeService.GenerateApiSecretAsync(id);
        }
    }
}