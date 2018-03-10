using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.Client;
using Crm.Exceptions;
using Crm.Mappers.User.Client;
using Crm.Models;
using Crm.Models.User.Client;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Clients")]
    public class ClientsController : BaseController
    {
        private readonly IClientDao _dao;

        public ClientsController(IClientDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Client/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientModel>> GetList(ClientParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ClientModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            result.IsDeleted = !result.IsDeleted;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}