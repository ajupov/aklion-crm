using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.Administration.Store;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Store;
using Aklion.Infrastructure.Utils.RandomGenerator;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Stores")]
    public class AdministrationStoreController : Controller
    {
        private readonly IStoreDao _storeDao;

        public AdministrationStoreController(IStoreDao storeDao)
        {
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
            var result = await _storeDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern)
        {
            var result = await _storeDao.GetForAutocompleteByNamePattern(pattern).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(StoreModel model)
        {
            var store = model.Map();

            await _storeDao.Create(store).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(StoreModel model)
        {
            var store = await _storeDao.Get(model.Id).ConfigureAwait(false);
            if (store == null)
            {
                return false;
            }

            model.Map(store);

            await _storeDao.Update(store).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _storeDao.Delete(id).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("GenerateApiSecret")]
        [AjaxErrorHandle]
        public async Task<string> GenerateApiSecret(int id)
        {
            var store = await _storeDao.Get(id).ConfigureAwait(false);
            if (store == null)
            {
                return string.Empty;
            }

            store.ApiSecret = RandomGenerator.GenerateAlphaNumbericString(16);

            await _storeDao.Update(store).ConfigureAwait(false);

            return store.ApiSecret;
        }
    }
}