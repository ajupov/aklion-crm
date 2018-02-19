using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ClientAttribute;
using Aklion.Crm.Mappers.Administration.ClientAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Clients
{
    [AjaxErrorHandle]
    [Route("Administration/ClientAttributes")]
    public class AdministrationClientAttributeController : BaseController
    {
        private readonly IClientAttributeDao _dao;

        public AdministrationClientAttributeController(IClientAttributeDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ClientAttributeModel>> GetList(ClientAttributeParameterModel model)
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
        public Task Create(ClientAttributeModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ClientAttributeModel model)
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