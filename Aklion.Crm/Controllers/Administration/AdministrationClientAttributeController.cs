using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ClientAttribute;
using Aklion.Crm.Mappers.Administration.ClientAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ClientAttributes")]
    public class AdministrationClientAttributeController : BaseController
    {
        private readonly IClientAttributeDao _clientAttributeDao;

        public AdministrationClientAttributeController(IClientAttributeDao clientAttributeDao)
        {
            _clientAttributeDao = clientAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeModel>> GetList(ClientAttributeParameterModel model)
        {
            var result = await _clientAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _clientAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ClientAttributeModel model)
        {
            return _clientAttributeDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ClientAttributeModel model)
        {
            var result = await _clientAttributeDao.GetAsync(model.Id).ConfigureAwait(false);

            await _clientAttributeDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _clientAttributeDao.DeleteAsync(id);
        }
    }
}