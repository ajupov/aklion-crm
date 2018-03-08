using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Client;
using Aklion.Crm.Mappers.Administration.Client;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class ClientsController : BaseController
    {
        private readonly IClientDao _dao;

        public ClientsController(IClientDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Client/Index.cshtml");
        }

        [HttpGet]
        public async Task<PagingModel<ClientModel>> GetList(ClientParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern, int storeId)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        public Task Create(ClientModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ClientModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}