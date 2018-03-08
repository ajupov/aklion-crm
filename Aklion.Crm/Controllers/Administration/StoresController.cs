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

        public IActionResult Index()
        {
            return View("~/Views/Administration/Store/Index.cshtml");
        }

        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew());
        }

        public Task Create(StoreModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        public async Task Update(StoreModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }

        public Task<string> GenerateApiSecret(int id)
        {
            return _service.GenerateApiSecretAsync(UserContext.UserId, UserContext.StoreId, id);
        }
    }
}