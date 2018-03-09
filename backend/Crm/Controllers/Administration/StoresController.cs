using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.Store;
using Crm.Dao.Store;
using Crm.Mappers.Administration.Store;
using Crm.Models;
using Crm.Models.Administration.Store;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class StoresController : BaseController
    {
        private readonly IStoreService _service;
        private readonly IStoreDao _dao;

        public StoresController(IStoreService service, IStoreDao dao)
        {
            _service = service;
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Store/Index.cshtml");
        }

        [HttpGet]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew());
        }

        [HttpPost]
        public Task Create(StoreModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(StoreModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }

        [HttpPost]
        public Task<string> GenerateApiSecret(int id)
        {
            return _service.GenerateApiSecretAsync(UserContext.UserId, UserContext.StoreId, id);
        }
    }
}