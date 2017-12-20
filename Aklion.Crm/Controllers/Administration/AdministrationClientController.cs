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
    [Route("Administration/Clients")]
    public class AdministrationClientController : BaseController
    {
        private readonly IClientDao _clientDao;

        public AdministrationClientController(IClientDao clientDao)
        {
            _clientDao = clientDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Client/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientModel>> GetList(ClientParameterModel model)
        {
            var result = await _clientDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _clientDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ClientModel model)
        {
            return _clientDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ClientModel model)
        {
            var result = await _clientDao.GetAsync(model.Id).ConfigureAwait(false);

            await _clientDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _clientDao.DeleteAsync(id);
        }
    }
}